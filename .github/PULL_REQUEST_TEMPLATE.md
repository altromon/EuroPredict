## 1. SDD Identification and Traceability

- **SDD Change Specification**: `specs/changes/active/<change-id>/` <!-- Link to active change directory -->
- **PDaC Handoff Sidecar**: `specs/changes/active/<change-id>/handoff.yaml` <!-- ID: HOF-* -->
- **Git Hierarchy Tier (4 Tiers)**:
  - [ ] Tier 4: `task/<PARENT-ID>/<TSK-ID>-<slug>` ➔ `feat/<PARENT-ID>-<slug>`
  - [ ] Tier 3: `feat/<FEAT-ID>-<slug>` ➔ `release/vX.Y.Z`
  - [ ] Tier 2: `release/vX.Y.Z` ➔ `main`
- **Linked Issues**: Closes #... / Refs #...

---

## 2. 📋 Plan Execution Matrix and Testing Status (`X` vs `O`)

> [!IMPORTANT]
> **Non-Negotiable Total Exhaustiveness Rule**:
> This table must include **each and every item identified for implementation** in the delivery plan (originating from `tasks.md`, the SDD specification, or issue criteria), without omitting any or creating abstract groupings.
>
> **Mandatory Marking Convention**:
> - **`[X]` (Completed and Tested)**: Item is 100% implemented and has verifiable automated tests on disk (`tests/` or `.feature`) passing green.
> - **`[O]` (Problem / Blocked)**: If there was any issue, technical limitation, failing test, or deferred scope, it must be marked with `[O]` and **full mandatory justification is required**:
>   1. **Technical root cause** or encountered limitation.
>   2. **Impact** on increment or system.
>   3. **Mitigation** or rationale for deferral.
>   4. **Formal reference** to tracking issue (GitHub Issue or `ADR-TECH-DEBT-*`).
>
> *Any PR with omitted planned items or unjustified `[O]` will be immediately blocked and rejected by the Tech Lead.*

| Task / Item ID | Planned Description | Status (`[X]` / `[O]`) | Test Evidence on Disk or Mandatory Justification of `[O]` |
| :--- | :--- | :---: | :--- |
| `TSK-001` | <!-- e.g. Contract DTOs and Interfaces Definition --> | **[X]** | Covered in `tests/unit/dto.spec.ts` (100% pass) |
| `TSK-002` | <!-- e.g. Network disconnection resilience --> | **[O]** | **Root cause:** TCP socket mock exhibits race conditions in CI.<br>**Impact:** Low in prod, flaky test.<br>**Mitigation:** Temporary extended timeout; tracked in issue #... |

---

## 3. 🗺️ Weak Points Map and Risk Hotspots (Weak Points Hotspots)

### A. Complexity Hotspots
<!-- Record functions nearing quality-policy.yaml thresholds: CC > 7 (max 10), Cognitive > 10 (max 15), LOC > 30 (max 40), MI < 70 (min 50/65) -->
| File | Function / Module | Metrics (CC / Cognitive / LOC / MI) | Technical Maintainability Justification |
| :--- | :--- | :---: | :--- |
| <!-- e.g. src/engine.ts --> | <!-- parsePayload() --> | CC: 8, Cog: 11, LOC: 32, MI: 72 | Deterministic parser without recursion; exhaustively tested. |

### B. Edge Cases and Blind Spots
<!-- Scenarios not covered by automated unit tests (concurrency, network timeouts, memory, jitter) -->
- [ ] **Edge Case 1**: <!-- Scenario description, estimated risk, and operational mitigation -->
- [ ] **Edge Case 2**: <!-- Scenario description, estimated risk, and operational mitigation -->

### C. AI Assumptions
<!-- Behaviors or heuristics assumed by the agent during implementation that require critical human review -->
- [ ] **Assumption 1**: <!-- e.g. Assumed payload always arrives pre-parsed in UTF-8 -->
- [ ] **Assumption 2**: <!-- e.g. Default timeout set to 5000ms as not stipulated in spec -->

---

## 4. 🛡️ Attack Surface and Cybersecurity (STRIDE / Zero Trust)

- [ ] **New External Channels**: Does it introduce HTTP endpoints, sockets, IPC channels, or brokers?
- [ ] **Sanitization and Inputs**: Are all external inputs validated with strict typing schemas (Zod/JSON Schema)?
- [ ] **Insecure Serialization**: Is polymorphic deserialization or dynamic code evaluation avoided?
- [ ] **Secrets and Enclaves**: Are cryptographic keys, credentials, sensitive env vars, or enclaves (`SEC-ENC-*`) handled?
- [ ] **Mitigated Security Requirements**: Implemented IDs (e.g. `SEC-REQ-*`) and associated tests (`SEC-TEST-*` or `.feature`).

---

## 5. 📦 Dependencies and Licenses Inspection

- **New Dependencies Added**:
  | Package | Version | License Type | `license-policy.yaml` Verification |
  | :--- | :---: | :---: | :---: |
  | <!-- e.g. zod --> | <!-- ^3.22.4 --> | <!-- MIT --> | <!-- ✔ Compliant (Permissive) --> |
- [ ] Zero dependencies with prohibited licenses (GPLv3/AGPL/unapproved commercial).
- [ ] Verification executed successfully (`pnpm run verify:licenses`).

---

## 6. ✅ Deterministic Pre-Flight Checklist (Release Gates)

- [ ] **Strict Typing and Linter**: `pnpm run typecheck` (zero errors, zero unjustified suppressions).
- [ ] **Quality Gate**: `pnpm run verify:quality` (CC $\le 10$, Cognitive $\le 15$, MI $\ge 50$, LOC $\le 40$).
- [ ] **BDD / Unit Tests**: `pnpm run verify:testing` (Gherkin scenarios synchronized and 100% passing).
- [ ] **360° Traceability**: `pnpm run verify:traceability` (no orphan requirements).
- [ ] **Open Source Licenses**: `pnpm run verify:licenses` (strict policy compliance).
- [ ] **Schema Compliance**: `pnpm run verify:schemas` (100% of artifacts conform to JSON Schema).
- [ ] **Full Test Suite**: `pnpm run test:all` / `pnpm run verify:all` (general green CI approval).

---

## 7. 🚨 Contingency Plan, Observability, and Rollback

- **Observability Metrics and Alarms**: <!-- What metrics (p99 latency, 5xx error rate, memory) or logs should be monitored post-deployment? -->
- **Rollback Procedure**: <!-- Exact command or procedure to revert change without collateral impact (e.g. git revert -m 1 <sha>) -->

---

## 8. 📊 AI-SDLC: Aggregated KPIs Summary

<!-- AI-SDLC-KPI-SUMMARY-START -->
| Metric | Human | Total PR |
| :--- | :---: | :---: |
| **Commits Made** | - | **-** |
| **Lines Added / Modif.** | - | **-** |
| **Active Development Time** | - | **-** |
| **Consumed Tokens (In+Out)** | - | **-** |
| **Estimated Cost (€/$)** | — (Labor) | **-** |

> *This block is updated automatically via `aisdlc kpi pr` or the CI workflow.*
<!-- AI-SDLC-KPI-SUMMARY-END -->
