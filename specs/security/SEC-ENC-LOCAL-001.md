---
id: SEC-ENC-LOCAL-001
type: security-enclave
title: Local Console Execution Enclave
status: draft
version: "1.0.0"
schema-version: "1.0"
trust-zone: trusted
trust-level: "Local user-space process trust boundary"
isolation: "OS process isolation for EuroPredict.UI.SimpleConsole and EuroPredict.Core"
perimeter-rules:
  - "Local CLI invocation by console user (ACT-ANALYST-001) with no inbound network listeners"
  - "Untrusted external data bytes must be parsed and validated before entering CombinationsCalculator"
  - "CLI arguments (args[0], args[1]) must be sanitized into bounded integer collections"
allowed-inbound:
  - SEC-ENC-INGEST-001
allowed-outbound:
  - SEC-ENC-INGEST-001
enforced-by:
  - SEC-REQ-INPUT-001
authentication-mechanism: none
supersedes: null
superseded-by: null
---

# SEC-ENC-LOCAL-001: Local Console Execution Enclave

## 1. Enclave Definition and Purpose
The **Local Console Execution Enclave** represents the local OS user-space process boundary where `EuroPredict.UI.SimpleConsole` (`Program.Main`) and the core statistical engine (`EuroPredict.Core.CombinationsCalculator`, `CombinationsSummaryAdapter`) execute. It protects the integrity of in-memory combination aggregation and CLI output generation from malformed or out-of-range inputs crossing from `SEC-ENC-INGEST-001` or CLI arguments.

## 2. Trust Zone and Perimeter Rules

| Parameter | Value | Security Justification |
| :--- | :--- | :--- |
| **Trust Zone** | `trusted` | Local single-user CLI execution with no exposed network ports. |
| **Authentication** | `none` | As-Is local workstation execution relies on OS user session. |

### Perimeter Rules
1. **No Inbound Network Listeners**: The application operates strictly as a short-lived CLI process.
2. **Boundary Validation**: All combination records returned from `SEC-ENC-INGEST-001` and CLI seed arguments (`args[0]`, `args[1]`) must be validated against EuroMillions domain bounds before frequency aggregation.
3. **Safe Failure**: Invalid CLI tokens or out-of-range numbers must be rejected without crashing or corrupting frequency counts.

---

## 3. Authorized Connectivity Matrix

| Direction | Entity Identifier | Type | Protocol / Channel |
| :--- | :--- | :--- | :--- |
| **Outbound** | `SEC-ENC-INGEST-001` | Enclave | In-process `ICombinationRepository.GetCombinations()` call |
| **Inbound** | `SEC-ENC-INGEST-001` | Enclave | Parsed `IEnumerable<ICombination>` in-memory return |

---

## 4. Linked Security Requirements

- `SEC-REQ-INPUT-001`: Validate EuroMillions domain ranges and CLI seed tokens.

---

## 5. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial As-Is security enclave for local console execution | RETRO-INCEPTION-001 |
