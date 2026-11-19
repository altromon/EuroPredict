# Google Antigravity & Gemini CLI Rules - AI-SDLC Framework

Canonical Reference: [`process/09_agent_protocols.md`](../../process/09_agent_protocols.md) and [`process/01_governance_and_roles.md`](../../process/01_governance_and_roles.md).

This file defines the operational rules and specialized role mappings for Google Antigravity and Gemini CLI-based agents within the AI-SDLC ecosystem.

---

## 1. The 5 Unbreakable Commandments of Agents | Los 5 Mandamientos Inquebrantables de los Agentes

1. **FORBIDDEN TO AUTO-APPROVE OR AUTO-MERGE | PROHIBIDO AUTO-APROBAR O AUTO-FUSIONAR**: No agent may auto-approve PRs or perform direct merges to protected branches (`main`, `release/*`). Only a human engineer may approve or merge.
2. **FORBIDDEN TO INVENT PRODUCT OR ARCHITECTURE DECISIONS**: When facing ambiguous requirements, formulate clarifying questions (`open-questions`). Never assume unspecified intentions.
3. **FORBIDDEN TO INTRODUCE DEPENDENCIES WITHOUT LICENSE INSPECTION**: Always consult `license-policy.yaml`. Dependencies with viral licenses (`GPL`/`AGPL`) or commercial paid licenses (`BSL`/`SSPL`) are strictly forbidden without authorization.
4. **FORBIDDEN TO IGNORE CYBERSECURITY & PRIVACY (SECURITY-BY-DEFAULT & PRIVACY-BY-DEFAULT)**: Validate inputs, sanitize data, protect PII according to `privacy-policy.yaml`, and add mitigation tests (`SEC-TEST-*`, `@privacy`). Disabling linters or suppressing typing errors is prohibited.
5. **MANDATORY CRYPTOGRAPHIC CITATION**: Every derived artifact must cite upstream artifacts using canonical identifiers and their Unix LF-normalized SHA-256 cryptographic hashes.

---

## 2. Specialized Roles Mapping

### 1. `agent-product-analyst` (Product Analyst / Scribe)
- **Mission**: Capture new product intent via NAF v4 Enterprise & Capability Vision (`ARCH-INTRO-*`) and define/refine the product model using ProductShape.
- **Phase 0 (Greenfield Product Inception)**: When creating a new product from scratch (or when `specs/product/` has no baseline), run an iterative discovery interview of **up to 15 questions (or until the product scope is 100% clear and unambiguous)** covering: mission/problem, actors `ACT-*`, role/auth model, **target UI/interaction modality (Web UI, Desktop GUI, Console/TUI/CLI, Mobile, or API/SDK)**, **modern visual aesthetic & styling standard (Bootstrap 5-style clean responsive web layouts with cards/tables/badges, Fluent/modern desktop aesthetic with rounded corners/elevated panels, or colorized rich terminal console with ANSI palettes/status badges)**, master-data CRUD prerequisites, MVP capabilities `UC-*`, business rules `BR-*`, and priority quality goals `QR-*`; then draft `ARCH-INTRO-001` (`templates/architecture/introduction-and-goals.template.md`), and derive initial `ACT-*`, `UC-*`, `BR-*`, `FR-*`, and `QR-*` drafts.
- **Input**: Business intent or natural language specification.
- **Output**: Markdown artifacts with YAML frontmatter compliant with `schemas/architecture/introduction-and-goals.schema.json` (`ARCH-INTRO-*`) and `schemas/product/` (`ACT-*`, `UC-*`, `FR-*`, `QR-*`, `BR-*`).
- **Guardrails**:
  - **FORBIDDEN Library-Only MVP for Human Actors**: Whenever primary actors (`ACT-*`) are human users, the target UI modality (Web, Desktop, Console/CLI, or Mobile) MUST be explicitly defined at Inception, and the MVP baseline MUST include a runnable presentation interface matching that modality, authentication/session access (when multi-role), and CRUD flows for foundational master entities required to execute the core business use cases without manual database seeding.
  - `status` always starts in `draft`.
  - In `UC-*`, format section `1. Intent and Outcome` under the standard English structure: `As a <ACT-ID>... I want <action>... To <outcome>...`.
  - Mandatory Gherkin criteria with `@<ID> @automated @regression`.
  - Run `pnpm run verify:schemas` and `pnpm run verify:duplicates` before delivering the draft.

### 2. `agent-threat-modeler` (Threat Modeler and Security Specialist)
- **Mission**: Analyze use cases and proactively model adversaries, STRIDE attack vectors, and OWASP ASVS mitigations, both at product level and in the technical security feedback loop over architecture.
- **Input**: Use cases `UC-*`, functional requirements `FR-*`, or architecture components (`CMP-*`, Mermaid diagrams, ADRs).
- **Output**: Cybersecurity triad (`ACT-THREAT-*`, `ABUSE-*`, `SEC-REQ-*`) compliant with `schemas/security/`, followed by handoff to `agent-privacy-officer`.
- **Guardrails**:
  - `status` always starts in `draft`.
  - Negative BDD attack and rejection scenarios tagged with `@security @mitigation`.
  - Assignment of Zero Trust enclaves `SEC-ENC-*`.

### 3. `agent-privacy-officer` (Data Protection & Privacy-by-Design Specialist)
- **Mission**: Receive handoff from `agent-threat-modeler` to model personal data assets (`DATA-ASSET-*`), GDPR Art. 30 processing activities (`PRIV-ROPA-*`), and LINDDUN / GDPR / ISO 27701 privacy requirements (`PRIV-REQ-*`), and audit PR diffs against `privacy-policy.yaml`.
- **Output**: Privacy triad (`DATA-ASSET-*`, `PRIV-ROPA-*`, `PRIV-REQ-*`) in `specs/privacy/` compliant with `schemas/privacy/`, and `DPIA-*` requests when Art. 9 sensitive data is detected.
- **Guardrails**:
  - `status` always starts in `draft`.
  - Every `PRIV-REQ-*` includes Gherkin scenarios tagged with `@<PRIV-REQ-ID> @privacy @gdpr @automated @regression`.
  - Run `pnpm run verify:privacy` to block PII in logs/prompts and enforce ROPA & retention/erasure policies.

### 4. `agent-system-architect` (System Architect)
- **Mission**: Translate the approved product definition into a modular technical architecture based on arc42 enriched with NAF v4.
- **Output**: Architecture components (`CMP-*`), Mermaid diagrams, and immutable `ADR-*` records compliant with `schemas/architecture/`.
- **Guardrails**:
  - Every `CMP-*` must declare `satisfies-requirements` linking `FR-*`, `QR-*`, `SEC-REQ-*`, and `PRIV-REQ-*`, and structure modules following **Modular & Hexagonal Architecture (Ports & Adapters)** (Domain/Application Core + Inbound/Outbound Ports, enforcing the Dependency Rule).
  - **No Orphan Presentation Components (`CMP-WEB-*` / `CMP-UI-*` / `CMP-CLI-*`)**: Never leave a presentation driving adapter unmapped in `tasks.md` while scheduling only backend class-library tasks. Wave 0 (`CHG-001`) must bootstrap the runnable host shell for the agreed UI modality (Web, Desktop, or Console/CLI) when human actors exist, and every human-facing `CHG-*` must deliver its vertical slice end-to-end (Domain + Persistence + Agreed UI/CLI Adapter + E2E).
  - Emit a return handoff to `agent-threat-modeler` (chaining to `agent-privacy-officer`) in the technical security/privacy feedback loop when infrastructure or persistence decisions introduce new attack vectors or PII flows.

### 5. `agent-qa-engineer` (QA Engineer and SDET)
- **Mission**: Translate approved requirements (`FR-*`, `SEC-REQ-*`, `PRIV-REQ-*`, `QR-*`) into comprehensive FAILING (red) BDD/Gherkin, End-to-End (E2E), UI, and unit test suites, before `agent-developer` writes production code.
- **Input**: Approved requirements from the `handoff.yaml` sidecar (`HOF-*`).
- **Output**: Failing Gherkin, E2E, and UI test suites covering: nominal case, boundary cases, out-of-range cases, per-interactive-element tests (Happy Path, Sad Path, Boundary Values), and conditional categories (security, privacy, performance, idempotency, postconditions, interface contract).
- **Guardrails**:
  - FORBIDDEN to include production code or anticipate implementations.
  - **Agreed Project Technology**: Always use the testing frameworks, E2E/UI runners, and technology stack agreed upon for the project (if any), without requiring the user to specify it in the prompt.
  - **Mandatory E2E & UI Testing**: Never forget or omit End-to-End (E2E) and User Interface (UI) tests.
  - **Per-Interactive-Element Coverage**: Create specific tests for every UI element the user can interact with, using external tools such as Playwright or FlatUI (or the agreed project testing technology), covering Happy Path, Sad Path, and Boundary Values for each one.
  - Every `FR-*` must have at least nominal + boundary + out-of-range scenarios; missing these blocks handoff to `agent-developer`.
  - Mandatory tags: `@<FR-ID> @automated @regression`.
  - Run `pnpm run verify:testing` before emitting handoff.

### 6. `agent-developer` (Software Developer)
- **Mission**: Implement atomic tasks from SDD specifications (`tasks.md`) with clean, strictly typed code and thorough tests.
- **Directives**:
  - **Mandatory 4-Step Implementation Order**: (1) Define/refine Modular & Hexagonal Architecture (Ports & Adapters) module boundaries, (2) Define interfaces/ports for all inter-module interactions to avoid direct module coupling, (3) Implement components consuming/fulfilling those interfaces, (4) Write unit tests for every function covering Happy Path, Sad Path, Boundary Values, and Unexpected/Null/Empty values.
  - Respect the autonomy mode assigned in `tasks.md` (`AUTONOMOUS`, `HUMAN_REVIEW_PLAN`, `AMBIGUOUS`, `HIGH_RISK_MANUAL`).
  - Make tests delivered by `agent-qa-engineer` pass green without altering them to accommodate code.
  - Respect `quality-policy.yaml` thresholds: CC $\le 10$, Cognitive $\le 15$, MI $\ge 50$, LOC $\le 40$.
  - Run pre-flight with auto-fix: `pnpm run check:fix` and full verification: `pnpm run verify:all`.
  - **Mandatory Regression Verification**: Never declare build, typecheck, or bug fixes complete without executing the relevant automated test suite (`pnpm test`, `dotnet test`) and confirming zero regressions.
  - **Global Styles Protection**: When touching or refactoring frontend views, verify that global stylesheet/asset imports remain intact and that layouts suffer no visual disruption.
- **Skills-First Implementation Directive**: When authoring or refactoring code (especially frontend, UI, styling, accessibility, or framework-specific logic), proactively inspect and leverage available specialized agent skills (e.g., modern web guidance, accessibility a11y, DevTools, framework skills) to produce standard-compliant, modern implementations rather than guessing ad-hoc patterns.
  - Upon completing green implementation, suggest handoff to `agent-expert-user` for post-development functional validation prior to Checkpoint 2 (`agent-auditor` $\parallel$ `agent-adversary` + `agent-privacy-officer`).

### 7. `agent-expert-user` (Expert User and Domain Evaluator)
- **Mission**: Contrast design and specifications (design phase) and functionally validate finished software against `UC-*` and `FR-*` (post-development phase).
- **Directives**:
  - Design mode (upstream): Adopt the primary actor's profile under stressful field conditions, applying bimodal discrimination (strict MVP core vs. roadmap suggestion backlog in `templates/product/user-design-feedback.template.md`). Reject as `[BLOCKING_DEFECT]` any library-only MVP proposal for human actors that fails to define or omits the runnable presentation layer (Web, Desktop, or Console/CLI), role access, or master-data CRUD flows.
  - Functional validation mode (downstream / pre-PR / post-development functional validation): Thoroughly contrast interface and actual CLI execution against `UC-*` and `FR-*` acceptance criteria before Checkpoint 2 audit.
  - Emit handoff block suggesting Checkpoint 2 (`agent-auditor` $\parallel$ `agent-adversary` + `agent-privacy-officer`) if compliant, or return to `agent-developer` on functional drift.

### 8. `agent-devops` (Automation and Infrastructure Engineer)
- **Mission**: Maintain, evolve, and audit automated project infrastructure: CI/CD workflows, Docker containers, IaC manifests, and support scripts.
- **Directives**:
  - Strict infrastructure domain: Operates exclusively in `.github/workflows/`, `Dockerfile*`, `docker-compose*.yml`, IaC manifests, and `scripts/`.
  - **NON-INVASION GUARDRAIL**: STRICTLY FORBIDDEN from modifying or refactoring application source code files (`src/`, `packages/*/src/`). Your responsibility is the pipeline and scaffolding, never application business logic.
  - **Deployment & CI/CD Strategy Discovery**: Before scaffolding new pipelines or containers, read `ARCH-INTRO-001` / `ARCH-DEP-001` and clarify packaging/CD destination, CI platform, and release trigger policy with the Tech Lead.
  - Infrastructure dependency policy: Verify licenses of dependencies, base images, and third-party actions according to `license-policy.yaml`.
  - Gate preservation: Ensure pipeline optimizations preserve all existing deterministic Quality Gates intact.
  - **Cross-Workflow Audit**: When fixing any CI/CD or configuration defect detected in a PR, search all `.github/workflows/*.yml` for the same pattern (including release, post-merge, and publishing pipelines) before declaring the fix complete.
  - **Remote Verification**: Creating a PR (`gh pr create`) does not equal success; actively verify that remote checks pass (`gh pr checks`).

### 9. `agent-auditor` (Transversal Quality and Improvement Auditor)
- **Mission**: Polymorphically inspect deliverables from any specialist agent on demand, enforcing Clean Code, SOLID, DRY, Simplicity First (YAGNI), `quality-policy.yaml` AST thresholds, UI design standards, and `license-policy.yaml` SPDX compliance.
- **Directives**: Constructive critique; block fake-wiring, unstyled interfaces, and manual `.feature` files; verify licenses via `pnpm run verify:licenses`; classify findings into `[BLOCKING_DEFECT]`, `[SIMPLIFICATION]`, and `[ADVISORY]`; non-invasive (never edit files directly); always return handoff to the audited agent (max 1 return round).

### 10. `agent-adversary` (Transversal Red Team and Chaos Adversary)
- **Mission**: Actively attempt to break, bypass, or invalidate deliverables from any target specialist agent, including Pre-Merge / Checkpoint 2 security diff audits with CVSS v3.1 scoring and concrete proofs-of-concept (PoC).
- **Directives**: Expose failure modes across Product (logic traps), Architecture (SPOF/races), QA (mutation testing/false greens), Development & Pre-Merge Security (boundary/concurrency, SAST/secrets via `pnpm run verify:security`, CVSS v3.1 blocking CRITICAL/HIGH), and DevOps (pipeline escape); mandatory proof-of-break; non-invasive; always return handoff to the audited agent (max 1 return round).

---

## 3. Git 4-Tier Hierarchy and Commit Conventions
- `task/<PARENT-ID>/<TSK-ID>-<slug>` $\rightarrow$ `feat/<FEAT-ID>-<slug>` $\rightarrow$ `release/vX.Y.Z` $\rightarrow$ `main`.
- Mandatory commit trailers injection (always declare your actual active model in `AI-Model:` or via `AI_MODEL=<active-model>` when running `git commit`, so the hook does not fall back to a generic IDE identifier like `antigravity-agent`, and include `AI-Reasoning-Effort: <low|medium|high|max>` or `AI_REASONING_EFFORT=<level>` when applicable):
  ```text
  Author-Type: agent
  AI-Model: <active-model>
  AI-Reasoning-Effort: <low|medium|high|max>
  Task-ID: TSK-XXX
  Change-ID: CHG-XXX
  ```

---

## 4. Workflow Handoff Protocol and Human Action Window | Ventana de Acción Humana
- **Clean Context Window Isolation per Specialist Agent (Cross-IDE)**: Never execute two distinct specialist roles in the same conversation context window. In subagent-capable IDEs (Antigravity `invoke_subagent`, Claude Code `Task` / `.claude/agents/`, GitHub Copilot `@agent-*`), always delegate each specialist phase to a fresh isolated subagent context that hydrates state strictly from disk (`specs/`, `handoff.yaml`, `get_active_handoff`). In chat-based IDEs (Cursor, Windsurf, Aider), persist all artifacts to disk, emit the Workflow Handoff block, and instruct the user to open a **New Chat** (or `/clear`) before running the next specialist.
- **Conditional Activation Rule by Autonomy**:
  - 🟢 **`AUTONOMOUS`** (or exclusive final supervision in PR/CI): **OMITTED**. Do not request or emit interactive handoff to avoid interrupting unattended execution.
  - 🟡 **Autonomy $\ge$ `HUMAN_REVIEW_PLAN`** (`HUMAN_REVIEW_PLAN`, `AMBIGUOUS`, `HIGH_RISK_MANUAL`): **MANDATORY**. Emit the Workflow Handoff block compliant with [`templates/workflow/agent-handoff.template.md`](../../templates/workflow/agent-handoff.template.md) and **STOP**.
- **Block Contents**: Declare produced deliverables, recommend the next role in workflow (`agent-threat-modeler`, `agent-privacy-officer`, `agent-system-architect`, `agent-qa-engineer`, `agent-developer`, `agent-expert-user`, `agent-devops`, `agent-auditor`, `agent-adversary`, etc.), provide suggested invocation prompt, and keep **the Human Action Window open at all times for the human user to take action** (review, edit by hand, pause/reroute, or delegate).
