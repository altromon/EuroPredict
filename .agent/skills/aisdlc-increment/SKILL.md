---
name: aisdlc-increment
description: >-
  Orchestrate the complete Spec-Driven Development (SDD) lifecycle for bounded feature increments, enhancements, and bugfixes (CHG-* / TSK-*), from upstream requirement derivation and streamlined Git branching through Red-Green-Refactor TDD, 2-checkpoint transversal auditor/adversary verification, all 12 Quality Gates, and canonical baseline integration.
---

# aisdlc-increment (Bounded SDD Feature & Bugfix Delivery Playbook)

Canonical Reference: [`process/06_spec_driven_development.md`](../../process/06_spec_driven_development.md), [`process/08_citation_contract_and_drift.md`](../../process/08_citation_contract_and_drift.md), [`process/10_quality_management_and_release_gates.md`](../../process/10_quality_management_and_release_gates.md), [`process/11_git_branching_and_lifecycle.md`](../../process/11_git_branching_and_lifecycle.md), and [`process/09_agent_protocols.md`](../../process/09_agent_protocols.md).

## Purpose & Trigger
Use this skill for **every evolutionary increment (new feature, enhancement, refactoring, or bugfix)** on a project that already has an AI-SDLC baseline. It enforces strict 360° traceability from upstream ProductShape (`UC-*`, `FR-*`) through SDD (`CHG-*`, `HOF-*`, `TSK-*`), TDD implementation, the 12 Quality Gates, and baseline promotion under the **streamlined 2-checkpoint architecture**.

## End-to-End Execution Playbook

### Step 0: Branch-Before-Change, Pre-Change Draft PR & Scope Triage (`patch | standard | critical`)
1. **Branch-Before-Change & Pre-Change Draft PR/MR**: Verify the active branch (`git branch --show-current`). If currently on a protected branch (`main`, `master`, `release/*`), create and switch to a dedicated working branch (`feat/<CHG-ID>-<slug>` or `fix/<CHG-ID>-<slug>`) **before** modifying or creating any files. Whenever a remote repository is configured (`git remote`), check if an open PR/MR already exists for the branch (`gh pr view` / `gh pr list --head <branch>` on GitHub; `glab mr view` / `glab mr list --source-branch <branch>` on GitLab); if none exists, create an empty bootstrap commit (`git commit --allow-empty -m "chore(<CHG-ID>): open draft PR"`), push (`git push -u origin <branch>`), and open a **Draft PR/MR** (`gh pr create --draft` on GitHub; `glab mr create --draft` or `git push -o merge_request.create -o merge_request.draft` on GitLab) before making any changes (if no remote exists, work locally).
2. Before editing any files or running `sdd_change_new`, inspect if the user explicitly specified the friction profile (`patch`, `standard`, or `critical`). If not, analyze the requested scope and ask the user via interactive question (`ask_question`) with the recommended option first (`(Recommended)`):
   - **`patch`**: low-risk isolated bugfixes ($\le 2$ source files, zero Anti-Bypass paths), typos, or linter fixes on `patch/*` or `fix/*` branches (`npx aisdlc change new "<Title>" --id <chg-id> --profile patch` generates `spec.md` with `profile: patch` and `verification` block only, omitting `design.md`, `tasks.md`, and `handoff.yaml`; fast-track directly to Step 3 -> Step 4 -> Step 5 -> Step 6).
   - **`standard`**: functional increments, new/updated `UC-*` or `FR-*`, or contract changes (`proposal.md`, `spec.md`, `design.md`, `tasks.md`, and `handoff.yaml`).
   - **`critical`**: changes touching security enclaves (`SEC-ENC-*`), sensitive PII (`SPECIAL_CATEGORY` / `DPIA-*`), schemas, or migrations (full `standard` artifacts + `ADR-*` + mandatory STRIDE modeling and dual human approval).
2. **Anti-Bypass Rule**: Never recommend or allow `patch` when the change touches protected paths (`schemas/`, `specs/security/`, `specs/privacy/`, `packages/security/`, `SEC-ENC-*`, `09_decisions/`, `migrations/`, or `*-policy.yaml`).

### Step 1: Upstream Product, Security & Privacy Delta (`agent-product-analyst`, `agent-threat-modeler` & `agent-privacy-officer`)
1. Inspect active changes via MCP tool `get_active_handoff` and verify whether the target Use Case (`UC-*`) or Requirement (`FR-*`) already exists in `specs/product/`.
2. If the increment introduces new behavior or modifies business rules:
   - Use [`agent-product-analyst`](../agent-product-analyst/SKILL.md) to create or update the `UC-*`, `BR-*`, and `FR-*` (with `@nominal`, `@boundary`, `@invalid` Gherkin scenarios).
   - Run MCP tool `verify_duplicates` (`npx aisdlc verify duplicates`) to block lexical or functional collisions early.
   - Use [`agent-threat-modeler`](../agent-threat-modeler/SKILL.md) to model any new `ABUSE-*` and `SEC-REQ-*` mitigations.
   - Hand off to [`agent-privacy-officer`](../agent-privacy-officer/SKILL.md) to model any personal data assets (`DATA-ASSET-*`), GDPR Art. 30 processing activities (`PRIV-ROPA-*`), and privacy requirements (`PRIV-REQ-*`).
3. Run `sdd_check_fix` (`npx aisdlc check --fix`) to sync Gherkin `.feature` files and validate schemas.

### Step 2: SDD Change Scaffolding & Architectural Design (`agent-system-architect`)
1. Scaffold the bounded change package and its cryptographic `handoff.yaml` sidecar (`HOF-*`) targeting **a single Use Case (`1 UC-* = 1 CHG-*`)** using MCP tool `sdd_change_new` (or CLI `npx aisdlc change new "<Title>" --from <UC-ID> --profile <patch|standard|critical>`).
2. Use [`agent-system-architect`](../agent-system-architect/SKILL.md) to complete `specs/changes/active/<chg-id>/design.md` and `specs/architecture/CMP-*.md`:
   - Structure `CMP-*` and `design.md` around **Modular & Hexagonal Architecture (Ports & Adapters)** (Domain/Application Core + Inbound/Outbound Ports) and define **Contract-First** shared interfaces/DTOs/schemas when spanning Frontend and Backend.
   - Map 100% of `handoff.yaml` requirements and components to atomic tasks (`TSK-*`) in `tasks.md` with explicit `autonomy-mode` (`AUTONOMOUS`, `HUMAN_REVIEW_PLAN`, `HIGH_RISK_MANUAL`, `AMBIGUOUS`).
3. Run `verify_governance` and `verify_friction` to validate task governance, 100% design/handoff scope coverage, and anti-monolith limits (`maxUseCasesPerChange = 1`).
4. Commit the SDD scaffolding and specification/design artifacts on the working branch and push (`git push`) to the open Draft PR.

### Checkpoint 1: Specification & Design Checkpoint (`agent-auditor` $\parallel$ `agent-adversary`)
1. Concurrently invoke `agent-auditor` (evaluating YAGNI, interface bloat, and task completeness) and `agent-adversary` (probing contradictions, race conditions, and architecture SPOFs) in clean, isolated context windows over the combined upstream deliverables.
2. Consolidate findings and remediate any blocking defects (max 2 correction rounds), committing and pushing updates (`git push`) to the open Draft PR.
3. **Human Approval Gate 1**: Present the specification and architectural design (reviewable directly in the Draft PR) for Human Tech Lead / PO sign-off before proceeding to code.

### Step 3: Streamlined Git Branch Navigation & Multi-Agent Parallelism
1. Confirm you are on the working branch via `npx aisdlc git checkout <TSK-ID>` (`pnpm run git:checkout <TSK-ID>`).
   - **Default Streamlined Mode**: Ensures the agent is on `feat/<CHG-ID>-<slug>` (already created in Step 0).
   - Granular task traceability is maintained via commit trailers (`Task-ID: TSK-*`, `Parent-Ref: CHG-*`) and commit scopes (`feat(TSK-001): ...`), enabling per-task rework KPIs without branch explosion.
2. **Parallel Agent Execution Rules (`--parallel`)**:
   - When concurrent multi-agent implementation is configured (e.g. Backend adapter $\parallel$ Frontend UI), `aisdlc git checkout <TSK-ID> --parallel` creates isolated worktrees (`task/<PARENT-ID>/<TSK-ID>-<slug>`).

### Step 4: Red Phase — Failing Test Suites (`agent-qa-engineer`)
1. Delegate to or adopt [`agent-qa-engineer`](../agent-qa-engineer/SKILL.md).
2. Run `npx aisdlc gherkin extract` (`sdd_check_fix`) to deterministically extract `.feature` files from Markdown specifications, and run `npx aisdlc gherkin scaffold <feature-file> --lang <lang> --fallback` to scaffold RED step definitions in `tests/steps/` (never hand-author `.feature` or step skeletons).
3. Author FAILING (red) assertions inside the scaffolded step definitions, plus integration/contract tests and mandatory E2E/UI tests (evaluating Happy Path, Sad Path, and Boundary Values for every interactive UI element, plus `UC-*` alternative/exception flows) in `tests/`.
4. Commit failing test suites with trailer `Task-ID: TSK-YYY` and push (`git push`) to the Draft PR (CI workflows skip heavy checks while the PR is in `draft` state).
5. Run `npx aisdlc verify gherkin` and `verify_testing` (`npx aisdlc verify testing`) to confirm deterministic Gherkin/step sync and requirement test coverage before writing production code.

### Step 5: Green & Refactor Phase — Surgical Implementation (`agent-developer` / `agent-devops`)
1. Delegate application tasks (`src/`, `tests/`) to [`agent-developer`](../agent-developer/SKILL.md). For tasks (`TSK-*`) targeting CI/CD workflows (`.github/workflows/`), `Dockerfile*`, IaC manifests, or support scripts (`scripts/`), delegate to [`agent-devops`](../agent-devops/SKILL.md) under its strict non-invasion guardrail on `src/`.
2. Execute the **Mandatory 4-Step Implementation Order**:
   - (1) Modular & Hexagonal Architecture (Ports & Adapters) boundaries, (2) Inter-module interfaces/ports first, (3) Concrete component implementation (zero fake-wiring; all 4 UI states `Loading`, `Empty`, `Error`, `Nominal`), (4) Exhaustive per-function unit tests in `tests/unit/` (Happy Path, Sad Path, Boundary, Null/Empty).
3. Commit implementations with trailer `Task-ID: TSK-YYY` and push (`git push`) incrementally to the Draft PR.
4. Verify any new dependency against `license-policy.yaml` via `verify_licenses` and PII handling against `privacy-policy.yaml` via `verify_privacy`.
5. Run `sdd_check_fix` (`npx aisdlc check --fix`) and `verify_quality` (`npx aisdlc verify quality`) until Cyclomatic Complexity $\le 10$, Cognitive $\le 15$, Maintainability $\ge 50$, and Function Length $\le 40$ lines pass with zero violations.
6. Run `verify` (`npx aisdlc verify all`) confirming all **12 Quality Gates** pass green.

### Checkpoint 2: Implementation & Pre-PR Checkpoint (`agent-auditor` $\parallel$ `agent-adversary`)
1. **Functional Validation (`agent-expert-user`)**: Validate live runtime behavior against `UC-*` and `FR-*` criteria (including `0` browser `console.error`/`pageerror` on UI changes).
2. Concurrently invoke `agent-auditor` (verifying Clean Code, SOLID, DRY, AST thresholds, and eliminating unnecessary abstractions) and `agent-adversary` (performing conceptual test mutation to verify QA tests catch defects, and attacking boundary inputs with concrete PoC breaks).
3. Consolidate findings and remediate in `agent-developer` (max 2 correction rounds), committing and pushing (`git push`) fixes to the Draft PR.
4. Run `npx aisdlc verify all` to confirm final green status, then run `report_quality` (`npx aisdlc report quality`) and `report_dashboard` (`npx aisdlc report dashboard`) to compute local reports and the interactive dashboard for review (**do NOT stage or commit global `reports/*` files on `feat/CHG-*` branches** to prevent merge conflicts across parallel PRs).

### Step 6: Human PR Sign-Off, Baseline Integration & Release Closure
1. **Ready for Review & Feature Merge (`feat/CHG-*` ➔ `main` or `release/vX.Y.Z`)**:
   - Update the Draft PR body with the Plan Execution Matrix (`[X]` vs `[O]`), KPI summary (`kpi_pr` including per-task rework metrics), and local verification/report summary (`reports/QUALITY_REPORT.md`, `reports/TRACEABILITY_MATRIX.md`, `reports/dashboard.html` inspected locally, without committing global `reports/*` to the feature PR), then run `gh pr ready` (GitHub) or `glab mr update --ready` (GitLab) to mark the PR/MR **Ready for Review** (triggering full CI verification) and present it to the human Tech Lead for review and approval (never auto-approve or auto-merge).
2. **Canonical Baseline Promotion**: Once tasks in `tasks.md` are marked `COMPLETED` and approved, promote the change into the canonical baseline via MCP tool `sdd_integrate` (`npx aisdlc sdd integrate --auto`), which synchronizes the normative text and BDD Gherkin scenarios into `FR-*` / `UC-*` / `SEC-REQ-*` / `PRIV-REQ-*`, bumps minor SemVer when updating active specs, resyncs PDaC SHA-256 digests, promotes `FR-*` to `active`, links `CMP-*`, and archives the change under `specs/changes/completed/` (or `npx aisdlc sdd integrate --all` to consolidate all completed changes).
3. **Semantic Versioning Promotion**:
   - **Profile-Driven SemVer Mapping**: Bump the version of the software under development according to the increment's friction profile:
     - `patch` -> bump **Patch** digit (`x.y.Z`)
     - `standard` -> bump **Minor** digit (`x.Y.0`)
     - `critical` -> bump **Major** digit (`X.0.0`)
   - **Highest-Precedence Consolidation (Multiple Pending Increments)**: When multiple pending increments exist in the active branch or release batch being integrated, apply **ONLY a single version bump corresponding to the highest rank** (`critical` / Major > `standard` / Minor > `patch` / Patch), ignoring unrelated historical directories.
   - Tag the merge with the computed SemVer bump (`patch`, `minor`, `major`) and refresh the interactive dashboard (`report_dashboard`).

## Non-Negotiable Guardrails
- **Clean Context Window Isolation per Specialist Agent (Cross-IDE)**: Never execute two distinct specialist roles (e.g., `agent-qa-engineer` and `agent-developer`, or `agent-developer` and `agent-adversary`) in the same conversation context window. In subagent-capable IDEs (Antigravity `invoke_subagent`, Claude Code `Task` / `.claude/agents/`, GitHub Copilot `@agent-*`), always spawn each specialist in a fresh isolated subagent context that reads state strictly from disk (`specs/`, `handoff.yaml`, `get_active_handoff`). In chat-based IDEs (Cursor, Windsurf, Aider), persist all artifacts to disk, emit the Workflow Handoff block, and instruct the user to open a **New Chat** (or `/clear`) before running the next specialist.
- **2-Checkpoint Quality & Adversarial Protocol**: Enforce transversal quality and red-team checks exclusively at **Checkpoint 1 (Specification & Design)** and **Checkpoint 2 (Implementation & Pre-PR)**. Enforce the **Anti-Ping-Pong Guardrail**: maximum 2 correction rounds per checkpoint; if unresolved blocking defects persist, escalate to the Human Tech Lead.
- **Single-Use-Case Scope (`1 UC-* = 1 CHG-*`)**: Each SDD change (`CHG-*`) must deliver at most 1 Use Case (`UC-*`). Bundling multiple `UC-*` into one `handoff.yaml` is blocked by `verify_governance` (`Greenfield monolith detected`). Scale throughput via parallel `CHG-*` branches/worktrees, not monolithic changes.
- **100% Design & Handoff Coverage in `tasks.md`**: Every `CMP-*`, `FR-*`, `SEC-REQ-*`, and `PRIV-REQ-*` declared in `design.md` and `handoff.yaml` must be explicitly assigned to at least one `TSK-*` in `tasks.md`.
