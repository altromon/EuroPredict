---
name: aisdlc-legacy-onboarding
description: Orchestrate progressive AI-SDLC adoption and Brownfield retro-inception on existing or legacy repositories without breaking production code, reverse-engineering architecture (ARCH-INTRO-*, CMP-*), 2-checkpoint auditor/adversary verification, and establishing baseline quality, security, and license audits before new SDD increments.
---

# aisdlc-legacy-onboarding (Legacy / Brownfield Retro-Inception Playbook)

Canonical Reference: [`process/02_product_definition.md`](../../process/02_product_definition.md), [`process/05_architecture_arc42_nafv4.md`](../../process/05_architecture_arc42_nafv4.md), [`process/06_spec_driven_development.md`](../../process/06_spec_driven_development.md), [`process/10_quality_management_and_release_gates.md`](../../process/10_quality_management_and_release_gates.md), and [`process/09_agent_protocols.md`](../../process/09_agent_protocols.md).

## Purpose & Trigger
Use this skill when onboarding an **existing codebase (Brownfield / Legacy)** that was not built with AI-SDLC. It establishes non-disruptive governance, maps existing code into canonical specifications, inventories debt across all 12 Quality Gates, and creates baseline specifications (`specs/product/`, `specs/architecture/`) before new features or refactorings are introduced.

## Retro-Inception Quality & Adversarial Checkpoint
Across Brownfield retro-inception, quality auditing and adversarial stress-testing are concentrated at the completion of retro-inception mapping:
- **Concurrent Inspection**: Concurrently invoke [`agent-auditor`](../agent-auditor/SKILL.md) (evaluating YAGNI, over-modeling, and debt priorities) and [`agent-adversary`](../agent-adversary/SKILL.md) (uncovering undocumented invariants, hidden failure modes, and security gaps) in clean, isolated context windows.
- **Non-Invasive Review**: Neither auditor nor adversary modifies deliverables directly; both deliver structured reports citing canonical IDs and SHA-256 digests.
- **Consolidated Remediation**: Consolidate findings and emit the Workflow Handoff back to authoring agents to resolve blocking defects and apply simplifications (max 2 correction rounds).
- **Anti-Ping-Pong Guardrail**: Bounded strictly to maximum **1 correction round**. If non-blocking advisories remain, proceed to characterization; if blocking defects persist, escalate to the Human Tech Lead.

## End-to-End Execution Playbook

### Step 1: Branch-Before-Change, Pre-Change Draft PR & Turnkey Repository Scaffolding
1. **Branch-Before-Change & Pre-Change Draft PR/MR**: Verify the active branch (`git branch --show-current`). If currently on a protected branch (`main`, `master`, `release/*`), create and switch to an onboarding branch (e.g., `chore/aisdlc-onboarding`) **before** modifying or creating any files. Whenever a remote repository is configured (`git remote`), check if an open PR/MR exists for the branch (`gh pr view` / `gh pr list --head <branch>` on GitHub; `glab mr view` / `glab mr list --source-branch <branch>` on GitLab); if none exists, create an empty bootstrap commit (`git commit --allow-empty -m "chore(onboarding): open draft PR"`), push (`git push -u origin <branch>`), and open a **Draft PR/MR** (`gh pr create --draft` on GitHub; `glab mr create --draft` or `git push -o merge_request.create -o merge_request.draft` on GitLab) before making any changes (if no remote exists, work locally).
2. Initialize AI-SDLC configuration without overwriting existing application files using `npx @aisdlc/cli init` (or MCP tool `new`).
3. Install the Git telemetry hook via `npx aisdlc git hook install` (`pnpm run git:hook:install`).
4. Invoke [`agent-devops`](../agent-devops/SKILL.md) to audit existing CI/CD workflows and container manifests and integrate `aisdlc verify all` into the pipeline without breaking legacy deployments.
5. Commit initial AI-SDLC schemas and policies (`.github/`, `schemas/`, `templates/`, `*-policy.yaml`) and push (`git push`) to the open Draft PR so subsequent retro-inception and characterization commits are pushed incrementally.

### Step 2: Automated Baseline Audit & Debt Inventory (Read-Only)
Before modifying any code or creating SDD changes, run diagnostic verifiers to quantify existing technical, legal, privacy, and security debt:
1. **License & AST Code Quality Baseline (`agent-auditor`)**: Run MCP tools `verify_licenses` (`npx aisdlc verify licenses`) and `report_quality` (`npx aisdlc report quality`) to identify viral (`GPL`/`AGPL`) or commercial (`BSL`/`SSPL`) dependencies and legacy hotspots exceeding Cyclomatic Complexity ($\le 10$), Cognitive Complexity ($\le 15$), or Maintainability Index ($\ge 50$).
2. **Shift-Left Security Scan (`agent-adversary`)**: Run MCP tool `verify_security` (`npx aisdlc verify security`) to detect hardcoded secrets (Gate 11) or OWASP SAST vulnerabilities in legacy files.
3. **Privacy & PII Scan (`agent-privacy-officer`)**: Run MCP tool `verify_privacy` (`npx aisdlc verify privacy`) to detect PII in logs/telemetry or hardcoded real PII in legacy tests/seeds.

### Step 3: Brownfield Retro-Inception (`agent-system-architect` & `agent-product-analyst`)
Instead of attempting to document 100% of a legacy monolith upfront, apply **bounded retro-inception** focused on the system boundary and the domain areas under active evolution:
1. **System Vision & Context (`ARCH-INTRO-001`)**: Use [`agent-product-analyst`](../agent-product-analyst/SKILL.md) to document the current system's mission, primary actors (`ACT-*`), and top quality goals (`QR-*`) in `specs/architecture/ARCH-INTRO-001.md`.
2. **As-Is Component Mapping (`CMP-*` & `ADR-*`)**: Use [`agent-system-architect`](../agent-system-architect/SKILL.md) to map existing core modules/services into `specs/architecture/CMP-*.md` and capture historical architectural constraints (`CON-*`, `ADR-*`).
3. **Core Use Cases Extraction (`UC-*`, `FR-*`)**: Extract canonical `UC-*` and `FR-*` artifacts in `specs/product/` for the specific bounded context or module that will be modified or extended next, including Gherkin acceptance scenarios reflecting current expected behavior (characterization specs).
4. **Threat & Privacy Boundary Mapping (`agent-threat-modeler` ➔ `agent-privacy-officer`)**: Map existing trust zones (`SEC-ENC-*`), active threat vectors (`ACT-THREAT-*`, `ABUSE-*`, `SEC-REQ-*`), and personal data inventories (`DATA-ASSET-*`, `PRIV-ROPA-*`, `PRIV-REQ-*`) on the target module.
5. **Retro-Inception Checkpoint (`agent-auditor` $\parallel$ `agent-adversary`)**: Concurrently inspect reverse-engineered artifacts, evaluate fidelity vs. unnecessary modeling overhead, and consolidate fixes.

### Step 4: Characterization Testing & Synchronization
1. Run MCP tool `sdd_check_fix` (`npx aisdlc check --fix`) to extract `.feature` files and compute canonical SHA-256 digests.
2. Run `verify_schemas` and `verify_pdac` to confirm the reverse-engineered baseline graph is structurally valid and drift-free.
3. Run `report_quality` (`npx aisdlc report quality`) and `report_dashboard` (`npx aisdlc report dashboard`) to generate the local baseline quality report and interactive dashboard for inspection (commit global `reports/` only on the onboarding baseline or post-merge on `main`, never on parallel feature PRs).
4. Push final baseline commits (`git push`) and mark the onboarding Draft PR/MR ready (`gh pr ready` / `glab mr update --ready`) for human Tech Lead review before merging to `main`.

### Step 5: Transition to Governed SDD Increments (Boy Scout / Strangler Rule)
Once the target domain's baseline artifacts (`ARCH-INTRO-*`, `UC-*`, `FR-*`, `CMP-*`) are in place, execute all new features, refactorings, or bugfixes via [`aisdlc-increment`](../aisdlc-increment/SKILL.md).

## Non-Negotiable Guardrails
- **Clean Context Window Isolation per Specialist Agent (Cross-IDE)**: Never execute two distinct specialist roles in the same conversation context window. Always spawn each specialist in a fresh isolated subagent context that reads state strictly from disk.
- **2-Checkpoint Quality & Adversarial Protocol**: Concentrated review at the retro-inception boundary and before PR delivery prevents process friction while maintaining rigorous defect interception.
- **Zero Drive-By Rewrites**: Never mass-refactor untouched legacy modules during onboarding. Apply the Strangler / Boy Scout rule: bring touched functions into compliance with `quality-policy.yaml` incrementally via governed SDD tasks (`TSK-*`).
- **Characterization First**: Never alter legacy business logic without first capturing its current behavior in an `FR-*` Gherkin specification and a verifiable test suite (`agent-qa-engineer`).
- **Immediate Secret Remediation**: If Step 2 (`verify_security`) uncovers leaked credentials (exit code 4), halt feature work and remediate/rotate secrets immediately.
