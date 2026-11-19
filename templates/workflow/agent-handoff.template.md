# Workflow Handoff Template

> **Canonical reference:** [`process/09_agent_protocols.md`](../../process/09_agent_protocols.md) and [`process/01_governance_and_roles.md`](../../process/01_governance_and_roles.md).
>
> **Activation Criterion by Autonomy Mode:**
> - 🟢 **`AUTONOMOUS` (or exclusive supervision at the end in PR/CI):** OMITTED. The agent executes and delivers without interrupting the user with this handoff block.
> - 🟡 **`HUMAN_REVIEW_PLAN` | 🟠 `AMBIGUOUS` | 🔴 `HIGH_RISK_MANUAL`:** MANDATORY. Upon completing the phase or proposal, the agent emits this block and **halts**, granting visibility to the user and relinquishing control.

---

### 🔄 Workflow Handoff

- **Completed Phase / Activity**: `[Current phase, e.g. Product Definition / Threat Modeling / Privacy Modeling / Architecture / SDD Implementation]`
- **Acting Role**: `[agent-product-analyst | agent-threat-modeler | agent-privacy-officer | agent-system-architect | agent-developer | agent-expert-user | agent-security-auditor | agent-compliance-checker]`
- **Task Autonomy Mode**: `[HUMAN_REVIEW_PLAN | AMBIGUOUS | HIGH_RISK_MANUAL]`
- **Produced Deliverables**:
  - [x] `[Path or ID of generated artifact, e.g. specs/product/uc-mission.md]`
  - [x] `[Path or ID of generated artifact, e.g. quality-report.md]`

- **Recommended Next Role(s) in Workflow**:
  - `[Suggested role name, e.g. agent-threat-modeler]`: `[Technical rationale, e.g. Model STRIDE adversaries and mitigate abuse cases]`
  - *(Optional)* `[Alternative role, e.g. agent-expert-user]`: `[Technical rationale, e.g. Evaluate UX and MVP boundary]`

- **Suggested Invocation Prompt**:
  > "Act as `[next-role]` and proceed with the `[next phase]` phase on the generated deliverables: `[key artifacts]`."
- **Clean Context Window Execution (Cross-IDE)**:
  - 🧹 **Subagent-Capable IDEs (Antigravity / Claude Code / Copilot)**: Delegate automatically via subagent tool (`invoke_subagent` / `Task` / `@agent-*`) to launch a fresh context window.
  - 🆕 **Chat-Based IDEs (Cursor / Windsurf / Aider)**: Open a **New Chat (`Cmd/Ctrl + N`)** or run `/clear` before pasting the Suggested Invocation Prompt so the next specialist starts with zero context pollution and hydrates state exclusively from `handoff.yaml` / `get_active_handoff`.

- **Human Action Window (User Sovereignty and Priority)**:
  - ✋ **Review and Validate**: Inspect generated artifacts and validate alignment with project vision before proceeding.
  - ✏️ **Edit Directly**: Modify requirements, code, or acceptance criteria directly at any time.
  - 🛑 **Pause, Divert, or Discard**: Freeze workflow, redirect toward another goal, or discard proposal.
  - ▶️ **Delegate to Next Agent**: If satisfied with output, execute suggested prompt to continue the chain.

---
