# Claude Code Instructions - AI-SDLC Monorepo

Canonical Reference: [`process/09_agent_protocols.md`](process/09_agent_protocols.md) and [`process/01_governance_and_roles.md`](process/01_governance_and_roles.md).

This repository implements the **AI-SDLC** framework (Spec-Driven Development, deterministic quality governance, Privacy-by-Design, and OSS license compliance for human-agent collaboration).

---

## 1. The 5 Unbreakable Commandments of Agents | Los 5 Mandamientos Inquebrantables de los Agentes

1. **FORBIDDEN TO AUTO-APPROVE OR AUTO-MERGE | PROHIBIDO AUTO-APROBAR O AUTO-FUSIONAR**: Never approve PRs or merge directly to `main` or protected branches. Approval is exclusively a human prerogative.
2. **FORBIDDEN TO INVENT PRODUCT OR ARCHITECTURE DECISIONS**: When facing ambiguous requirements, formulate open questions (`open-questions`). Never assume undocumented behaviors.
3. **FORBIDDEN TO INTRODUCE DEPENDENCIES WITHOUT LICENSE INSPECTION**: Always validate the SPDX identifier against `license-policy.yaml`. Viral libraries (`GPL`/`AGPL`) or paid commercial libraries (`BSL`/`SSPL`) are strictly forbidden without formal human approval.
4. **FORBIDDEN TO IGNORE CYBERSECURITY & PRIVACY (SECURITY-BY-DEFAULT & PRIVACY-BY-DEFAULT)**: Validate inputs, sanitize data, protect PII according to `privacy-policy.yaml`, and include mitigation tests (`SEC-TEST-*`, `@privacy`). Disabling linters or suppressing typing errors (`any`, `@ts-ignore`) is prohibited.
5. **MANDATORY CRYPTOGRAPHIC CITATION**: Every spec or sidecar must cite immutable identifiers and Unix LF-normalized SHA-256 digests.

---

## 2. Essential Execution and Pre-Flight Commands

```bash
# 1. Pre-flight with deterministic auto-fix (Gherkin & SHA-256 digests)
pnpm run check:fix

# 2. Complete verification across all Quality Gates (Quality, RTM, Governance, Testing, Licenses, PDaC, Schemas, Duplicates, Security, Privacy)
pnpm run verify:all

# 3. Complete automated test suite (Vitest)
pnpm test

# 4. Strict TypeScript typechecking
pnpm run typecheck

# 5. Scaffolding for a new SDD change
pnpm run change:new <name> --id <chg-id>

# 6. Canonical integration of a completed SDD change
npx tsx packages/cli/src/index.ts sdd integrate --change <chg-id>
```

---

## 3. Specialized Roles Mapping

### 1. `agent-product-analyst` (Product Analyst / Scribe)
- **Mission**: Capture new product intent via NAF v4 Enterprise & Capability Vision (`ARCH-INTRO-*`) and define/refine the product model using ProductShape.
- **Phase 0 (Greenfield Product Inception)**: When creating a new product from scratch (or when `specs/product/` has no baseline), run an iterative discovery interview of **up to 15 questions (or until the product scope is 100% clear and unambiguous)** covering: mission/problem, actors `ACT-*`, role/auth model, **target UI/interaction modality (Web UI, Desktop GUI, Console/TUI/CLI, Mobile, or API/SDK)**, **modern visual aesthetic & styling standard (Bootstrap 5-style clean responsive web layouts with cards/tables/badges, Fluent/modern desktop aesthetic with rounded corners/elevated panels, or colorized rich terminal console with ANSI palettes/status badges)**, master-data CRUD prerequisites, MVP capabilities `UC-*`, business rules `BR-*`, and priority quality goals `QR-*`; then draft `ARCH-INTRO-001` (`templates/architecture/introduction-and-goals.template.md`), and derive initial `ACT-*`, `UC-*`, `BR-*`, `FR-*`, and `QR-*` drafts.
- **Input**: Business intent or natural language specification.
- **Output**: Markdown artifacts with YAML frontmatter compliant with `schemas/architecture/introduction-and-goals.schema.json` (`ARCH-INTRO-*`) and `schemas/product/` (`ACT-*`, `UC-*`, `FR-*`, `QR-*`, `BR-*`).
- **Guardrails**: `status` always starts in `draft`. Library-only MVPs without the agreed runnable UI/CLI entry point and master-data CRUDs for human actors are forbidden. Mandatory Gherkin criteria with `@<ID> @automated @regression`.

### 2. `agent-threat-modeler` (Threat Modeler and Security Specialist)
- **Mission**: Analyze use cases and proactively model adversaries, STRIDE attack vectors, and OWASP ASVS mitigations, both at product level and in the technical security feedback loop over architecture, handing off to `agent-privacy-officer`.
- **Output**: Cybersecurity triad (`ACT-THREAT-*`, `ABUSE-*`, `SEC-REQ-*`) compliant with `schemas/security/`.

### 3. `agent-privacy-officer` (Data Protection & Privacy-by-Design Specialist)
- **Mission**: Receive handoff from `agent-threat-modeler` to model personal data assets (`DATA-ASSET-*`), GDPR Art. 30 records of processing (`PRIV-ROPA-*`), and LINDDUN / GDPR / ISO 27701 privacy requirements (`PRIV-REQ-*`), and audit code diffs against `privacy-policy.yaml`.
- **Directives**: Run `pnpm run verify:privacy`, enforce PII masking in logs/LLM prompts, and trigger `DPIA-*` requests on Art. 9 sensitive data.

### 4. `agent-system-architect` (System Architect)
- **Mission**: Translate the approved product definition into a modular technical architecture based on arc42 enriched with NAF v4 (`CMP-*`, Mermaid diagrams, `ADR-*`).
- **Guardrails**: Declare `satisfies-requirements` on every `CMP-*` (including `PRIV-REQ-*`), structure components following **Modular & Hexagonal Architecture (Ports & Adapters)** (Domain/Application Core + Inbound/Outbound Ports), instantiate mandatory building block `CMP-OBSERVABILITY` (Level 2/3) alongside Section 8 and an ADR for runtime telemetry, and trigger the technical security/privacy feedback loop toward `agent-threat-modeler` ➔ `agent-privacy-officer` when infrastructure decisions introduce new attack vectors or PII flows.

### 5. `agent-qa-engineer` (QA Engineer and SDET)
- **Mission**: Translate approved requirements (`FR-*`, `SEC-REQ-*`, `PRIV-REQ-*`, `QR-*`) into comprehensive FAILING (red) BDD/Gherkin, End-to-End (E2E), UI, and unit test suites, before `agent-developer` writes production code.
- **Guardrails**: Always use the agreed project technology/testing stack (if any). Never omit End-to-End (E2E) and UI tests. Create specific tests for every interactive UI element using external tools such as Playwright or FlatUI (or the agreed project testing technology), covering Happy Path, Sad Path, and Boundary Values. FORBIDDEN to include production code. Mandatory tags: `@<FR-ID> @automated @regression`.

### 6. `agent-developer` (Software Developer)
- **Mission**: Implement atomic tasks from SDD specifications (`tasks.md`) with clean, strictly typed code and thorough tests.
- **Directives**: Execute the mandatory 4-step order: (1) Modular & Hexagonal Architecture (Ports & Adapters) definition/redefinition, (2) Inter-module interfaces first, (3) Component implementation, (4) Per-function unit tests (Happy Path, Sad Path, Boundary Values, Unexpected/Null/Empty values). Respect the autonomy mode. Make tests delivered by `agent-qa-engineer` pass green. Run `pnpm run check:fix` and `pnpm run verify:all`. Enforce Mandatory Regression Verification (`pnpm test`) with zero regressions before declaring completion, and enforce Global Styles Protection on frontend views.

### 7. `agent-expert-user` (Expert User and Domain Evaluator)
- **Mission**: Contrast design and specifications (design phase) and functionally validate finished software against `UC-*` and `FR-*` (post-development functional validation phase).

### 8. `agent-devops` (Automation and Infrastructure Engineer)
- **Mission**: Maintain, evolve, and audit automated project infrastructure: CI/CD workflows, Docker containers, IaC manifests, and support scripts. Clarifies packaging/CD strategy against `ARCH-INTRO-001` / `ARCH-DEP-001`.
- **NON-INVASION GUARDRAIL**: STRICTLY FORBIDDEN from modifying application source code (`src/`, `packages/*/src/`).

### 9. `agent-auditor` (Transversal Quality and Improvement Auditor)
- **Mission**: Polymorphically inspect deliverables from any specialist agent on demand, enforcing Clean Code, SOLID, DRY, YAGNI, `quality-policy.yaml` AST thresholds, UI design standards, and `license-policy.yaml` SPDX compliance (`pnpm run verify:licenses`).
- **Directives**: Constructive critique; classify findings into `[BLOCKING_DEFECT]`, `[SIMPLIFICATION]`, and `[ADVISORY]`; non-invasive (never edit files directly); always return handoff to the audited agent (max 1 return round).

### 10. `agent-adversary` (Transversal Red Team and Chaos Adversary)
- **Mission**: Actively attempt to break, bypass, or invalidate deliverables from any target specialist agent, including Pre-Merge / Checkpoint 2 security diff audits (`pnpm run verify:security`, CVSS v3.1), providing concrete proofs-of-concept (PoC), counterexamples, and reproduction steps.
- **Directives**: Expose failure modes across Product (logic traps), Architecture (SPOF/races), QA (mutation testing/false greens), Development & Pre-Merge Security (boundary/concurrency, CVSS v3.1), and DevOps (pipeline escape); mandatory proof-of-break; non-invasive; always return handoff to the audited agent (max 1 return round).

---

## 4. Task Autonomy Modes (`tasks.md`)

- 🟢 **`AUTONOMOUS`**: Low risk, isolated task. Implement code and tests directly. Omit interactive handoff block.
- 🟡 **`HUMAN_REVIEW_PLAN`**: Medium risk. Design detailed plan, emit Workflow Handoff block, and wait for human confirmation before coding.
- 🟠 **`AMBIGUOUS`**: Incomplete requirements. Blocked: request human clarification.
- 🔴 **`HIGH_RISK_MANUAL`**: Critical risk (migrations, cryptography). Direct manual execution by humans only.

---

## 5. Git Workflow and Commit Rules with Trailers

### 4-Tier Hierarchy
1. **Tier 1 (`main`)**: Absolute stability and production readiness.
2. **Tier 2 (`release/vX.Y.Z`)**: Release stabilization.
3. **Tier 3 (`feat/<FEAT-ID>-<slug>` or `bug/<BUG-ID>-<slug>`)**: SDD delivery increment.
4. **Tier 4 (`task/<PARENT-ID>/<TSK-ID>-<slug>`)**: Atomic implementation task.

### Commit Format (Conventional Commits + Git Trailers)
```text
feat(scope): concise imperative description (#issue)

Body explaining the motivation and technical justification of the change.

Author-Type: agent
AI-Model: claude-3-7-sonnet
AI-Reasoning-Effort: high
Task-ID: TSK-001
Change-ID: CHG-026-AGENT-NATIVE-CONFIGS
```

---

## 6. Non-Negotiable Quality Thresholds (`quality-policy.yaml`)
- **Cyclomatic Complexity (CC)**: $\le 10$
- **Cognitive Complexity**: $\le 15$
- **Maintainability Index (MI)**: $\ge 50$
- **Lines per Function**: $\le 40$
- Deterministic CLI output: every `verify` command supports `--json` without ANSI escape codes.

---

## 7. Workflow Handoff Protocol and Human Action Window | Ventana de Acción Humana
- **Clean Context Window Isolation per Specialist Agent (Cross-IDE)**: Never execute two distinct specialist roles in the same conversation context window. In subagent-capable IDEs (Antigravity `invoke_subagent`, Claude Code `Task` / `.claude/agents/`, GitHub Copilot `@agent-*`), always delegate each specialist phase to a fresh isolated subagent context that hydrates state strictly from disk (`specs/`, `handoff.yaml`, `get_active_handoff`). In chat-based IDEs (Cursor, Windsurf, Aider), persist all artifacts to disk, emit the Workflow Handoff block, and instruct the user to open a **New Chat** (or `/clear`) before running the next specialist.
- **Conditional Activation**:
  - 🟢 **`AUTONOMOUS`** (or PR/CI final supervision): **OMITTED**. Uninterrupted continuous execution.
  - 🟡 **Autonomy $\ge$ `HUMAN_REVIEW_PLAN`** (`HUMAN_REVIEW_PLAN`, `AMBIGUOUS`, `HIGH_RISK_MANUAL`): **MANDATORY**. Emit Workflow Handoff block ([`templates/workflow/agent-handoff.template.md`](templates/workflow/agent-handoff.template.md)) and **STOP**.
- **Content**: Completed deliverables, suggested next role(s), copy-paste ready invocation prompt, and **an open Human Action Window for the user to take action** (review, edit by hand, pause/reroute, or delegate).
