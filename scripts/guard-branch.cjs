#!/usr/bin/env node
/**
 * AI-SDLC: Antigravity PreToolUse Branch, Coding Rules & SDD Increment Guard Hook
 * 1. Prevents AI agents from modifying or writing files directly on protected branches (main, master, release/*).
 * 2. Prevents AI agents from modifying or weakening deployed coding rules files once created.
 * 3. Enforces an active SDD increment in specs/changes/active/ before modifying source, test, or Anti-Bypass paths.
 */

const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');

const PROTECTED_CODING_RULES_FILES = [
  'Directory.Build.props',
  'tsconfig.strict.json',
  'eslint.config.mjs',
  'gradle/init.d/aisdlc-strict.init.gradle.kts',
  'config/pmd/aisdlc-ruleset.xml',
  'config/detekt/detekt.yml',
  '.cargo/config.toml',
  '.clippy.toml',
  '.golangci.yml',
  '.clang-tidy',
  'cmake/AisdlcStrictCompiler.cmake',
  '.github/CODEOWNERS',
];

const PROTECTED_BYPASS_PATTERNS = [
  /^schemas\//i,
  /^examples\/security\//i,
  /^specs\/security\//i,
  /^specs\/privacy\//i,
  /^packages\/security\//i,
  /enclaves?\//i,
  /SEC-ENC-/,
  /quality-policy\.ya?ml$/i,
  /license-policy\.ya?ml$/i,
  /privacy-policy\.ya?ml$/i,
  /08_security_concept\.md$/i,
  /09_decisions\//i,
  /migrations?\//i,
  /^\.github\/CODEOWNERS$/,
];

const STANDARD_SDD_ARTIFACTS = ['spec.md', 'design.md', 'tasks.md', 'handoff.yaml'];

function getWorkingBranch(cwd) {
  try {
    const sym = execSync('git symbolic-ref --short HEAD', {
      cwd,
      encoding: 'utf-8',
      stdio: ['pipe', 'pipe', 'ignore'],
    }).trim();
    if (sym) return sym;
  } catch {}

  try {
    return execSync('git rev-parse --abbrev-ref HEAD', {
      cwd,
      encoding: 'utf-8',
      stdio: ['pipe', 'pipe', 'ignore'],
    }).trim();
  } catch {}

  return '';
}

function isProtectedCodingRulesTarget(targetPath, cwd) {
  if (!targetPath) return false;
  const resolved = path.resolve(cwd, targetPath);
  const norm = resolved.replace(/\\/g, '/');
  const rel = path.relative(cwd, resolved).replace(/\\/g, '/');
  return (
    PROTECTED_CODING_RULES_FILES.some(
      (ruleFile) => rel === ruleFile || norm.endsWith('/' + ruleFile)
    ) || rel.startsWith('templates/coding-rules/')
  );
}

function isSourceOrTestTarget(relPath) {
  if (!relPath) return false;
  return /^(src|lib|packages|tests)\//.test(relPath.replace(/\\/g, '/'));
}

function isProtectedBypassPath(relPath) {
  if (!relPath) return false;
  const norm = relPath.replace(/\\/g, '/');
  return PROTECTED_BYPASS_PATTERNS.some((re) => re.test(norm));
}

function detectChangeDirProfile(changeDir) {
  for (const fileName of ['spec.md', 'proposal.md', 'handoff.yaml']) {
    const fullPath = path.join(changeDir, fileName);
    if (!fs.existsSync(fullPath)) continue;
    try {
      const content = fs.readFileSync(fullPath, 'utf-8');
      const match = content.match(/^\s*profile:\s*['"]?(patch|standard|critical)['"]?\s*$/m);
      if (match) return match[1];
    } catch {}
  }
  return 'standard';
}

function extractBranchChangePrefix(branch) {
  if (!branch) return '';
  const match = String(branch).match(/((?:chg|patch)-\d+)/i);
  return match ? match[1].toLowerCase() : '';
}

function matchesBranchPrefix(dirName, branchPrefix) {
  if (!branchPrefix) return true;
  const lower = dirName.toLowerCase();
  return lower === branchPrefix || lower.startsWith(branchPrefix + '-');
}

function listActiveChangeDirs(activeRoot, branchPrefix) {
  if (!fs.existsSync(activeRoot)) return [];
  try {
    return fs
      .readdirSync(activeRoot, { withFileTypes: true })
      .filter((entry) => entry.isDirectory() && matchesBranchPrefix(entry.name, branchPrefix))
      .map((entry) => path.join(activeRoot, entry.name));
  } catch {
    return [];
  }
}

function isSatisfiedChangeDir(changeDir, requiresFullSdd) {
  const profile = detectChangeDirProfile(changeDir);
  if (profile === 'patch') {
    return !requiresFullSdd && fs.existsSync(path.join(changeDir, 'spec.md'));
  }
  return STANDARD_SDD_ARTIFACTS.every((f) => fs.existsSync(path.join(changeDir, f)));
}

function validateProtectedBypassOnly(candidateDirs, relTargetPath) {
  const hasPatchCandidate = candidateDirs.some(
    (dir) => detectChangeDirProfile(dir) === 'patch'
  );
  if (hasPatchCandidate) {
    return {
      valid: false,
      reason: `❌ [AI-SDLC Anti-Bypass Guard] BLOCKED: Active SDD increment uses 'patch' profile, but '${relTargetPath}' is a protected Anti-Bypass path. Protected paths require a 'standard' or 'critical' SDD increment in specs/changes/active/ (run aisdlc-increment).`,
    };
  }
  return { valid: true };
}

function validateSourceOrTestEdit(candidateDirs, branchPrefix, relTargetPath, isBypass) {
  if (candidateDirs.length === 0) {
    const prefixHint = branchPrefix ? ` matching '${branchPrefix}'` : '';
    return {
      valid: false,
      reason: `❌ [AI-SDLC SDD Increment Guard] BLOCKED: Cannot modify '${relTargetPath}' without an active SDD increment${prefixHint} in specs/changes/active/. Run the 'aisdlc-increment' skill (Step 0 -> sdd_change_new) first.`,
    };
  }
  if (candidateDirs.some((dir) => isSatisfiedChangeDir(dir, isBypass))) {
    return { valid: true };
  }
  if (isBypass) {
    return {
      valid: false,
      reason: `❌ [AI-SDLC Anti-Bypass Guard] BLOCKED: Protected Anti-Bypass path '${relTargetPath}' cannot be modified under a 'patch' increment. A 'standard' or 'critical' SDD increment with spec.md, design.md, tasks.md, and handoff.yaml in specs/changes/active/ is required (aisdlc-increment).`,
    };
  }
  return {
    valid: false,
    reason: `❌ [AI-SDLC SDD Increment Guard] BLOCKED: Active SDD increment in specs/changes/active/ is incomplete for standard/critical profile (requires spec.md, design.md, tasks.md, and handoff.yaml) before editing '${relTargetPath}'. Complete SDD scaffolding via aisdlc-increment first.`,
  };
}

function hasValidActiveSddIncrement(cwd, branch = '', relTargetPath = 'src/') {
  if (!relTargetPath || relTargetPath.startsWith('..')) return { valid: true };
  if (!fs.existsSync(path.join(cwd, 'specs', 'changes'))) return { valid: true };

  const isSourceOrTest = isSourceOrTestTarget(relTargetPath);
  const isBypass = isProtectedBypassPath(relTargetPath);
  if (!isSourceOrTest && !isBypass) return { valid: true };

  const branchPrefix = extractBranchChangePrefix(branch);
  const activeRoot = path.join(cwd, 'specs', 'changes', 'active');
  const candidateDirs = listActiveChangeDirs(activeRoot, branchPrefix);

  if (!isSourceOrTest) return validateProtectedBypassOnly(candidateDirs, relTargetPath);
  return validateSourceOrTestEdit(candidateDirs, branchPrefix, relTargetPath, isBypass);
}

function isEphemeralPath(normPath) {
  return (
    normPath.includes('/.gemini/') ||
    normPath.includes('/brain/') ||
    normPath.includes('/scratch/')
  );
}

function evaluateGuardDecision(payload) {
  const targetPath = payload.toolCall?.args?.Path || '';
  const cwd = (payload.workspacePaths || [])[0] || process.cwd();
  const resolvedPath = targetPath ? path.resolve(cwd, targetPath) : '';
  const normPath = resolvedPath.replace(/\\/g, '/');

  if (isProtectedCodingRulesTarget(resolvedPath, cwd)) {
    return {
      decision: 'deny',
      reason: `❌ [AI-SDLC Immutable Coding Rules Guard] BLOCKED: AI agents are forbidden from modifying protected coding rules file '${path.basename(targetPath)}'. Fix the source or test code to satisfy the compiler/linter rules instead of altering the rule configuration.`,
    };
  }
  if (isEphemeralPath(normPath)) return { decision: 'allow' };

  const branch = getWorkingBranch(cwd);
  if (/^(main|master|release\/.*)$/.test(branch)) {
    return {
      decision: 'deny',
      reason: `❌ [AI-SDLC Security Guard] BLOCKED: Direct file modifications on protected branch '${branch}' are forbidden. You must create and checkout a new working branch first before editing files (e.g. 'git checkout -b feat/<CHG-ID>-<slug>' or 'git checkout -b task/<PARENT-ID>/<TSK-ID>-<slug>').`,
    };
  }

  const relPath = resolvedPath ? path.relative(cwd, resolvedPath).replace(/\\/g, '/') : '';
  const sddCheck = hasValidActiveSddIncrement(cwd, branch, relPath);
  if (!sddCheck.valid) return { decision: 'deny', reason: sddCheck.reason };

  return { decision: 'allow' };
}

function main() {
  let input = '';
  process.stdin.setEncoding('utf-8');
  process.stdin.on('data', (chunk) => {
    input += chunk;
  });
  process.stdin.on('end', () => {
    try {
      const payload = input.trim() ? JSON.parse(input) : {};
      console.log(JSON.stringify(evaluateGuardDecision(payload)));
    } catch {
      console.log(JSON.stringify({ decision: 'allow' }));
    }
  });
}

if (require.main === module) {
  main();
}

module.exports = {
  evaluateGuardDecision,
  hasValidActiveSddIncrement,
  detectChangeDirProfile,
  isSourceOrTestTarget,
  isProtectedBypassPath,
  isProtectedCodingRulesTarget,
};
