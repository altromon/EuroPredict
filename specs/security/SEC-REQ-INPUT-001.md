---
id: SEC-REQ-INPUT-001
type: security-requirement
title: Validate EuroMillions Domain Ranges and CLI Seed Tokens
status: draft
version: "1.0.0"
schema-version: "1.0"
security-domain: input-validation
mitigates-abuse-case:
  - ABUSE-TAMPER-INPUT-001
enforced-in-enclave: SEC-ENC-LOCAL-001
compliance-references:
  - "OWASP-ASVS-V5.1.1"
  - "OWASP-ASVS-V5.1.3"
acceptance-format: gherkin
cucumber-tags:
  - "@SEC-REQ-INPUT-001"
  - "@input-validation"
supersedes: null
superseded-by: null
---

# SEC-REQ-INPUT-001: Validate EuroMillions Domain Ranges and CLI Seed Tokens

## 1. Technical Control Statement
> **Target / To-Be Security Remediation Requirement**: Not yet implemented in the As-Is legacy codebase (pending a future `CHG-*` hardening increment).

The system MUST validate that all ingested draw numbers and CLI seed arguments entering `SEC-ENC-LOCAL-001` conform to EuroMillions domain bounds (5 distinct Columns in `1..50` and 2 distinct Stars in `1..12`), and MUST explicitly reject malformed or out-of-range CLI seed tokens rather than silently falling back to an unconstrained prediction.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product / Threat (Upstream)** | `ABUSE-TAMPER-INPUT-001` | Mitigates Abuse Case | Validated |
| **Enclave** | `SEC-ENC-LOCAL-001` | Enforced in Enclave | Validated |

---

## 3. Mitigation Criteria in Gherkin Format (Cucumber Security Tests)

```gherkin
@SEC-REQ-INPUT-001 @input-validation
Feature: EuroMillions Domain and CLI Seed Input Validation
  As a Security Officer
  I want EuroPredict to enforce domain range bounds on draw data and CLI seed arguments
  So that out-of-range numbers or malformed seeds cannot corrupt frequency predictions

  @pending-remediation
  Scenario Outline: Out-of-range draw numbers in data source are rejected
    Given a parsed draw row containing Column "<col_value>" and Star "<star_value>"
    When domain validation evaluates the draw record
    Then the record must be rejected as invalid
    And it must not be included in CombinationsSummaryAdapter frequency counts

    Examples:
      | col_value | star_value |
      | 0         | 5          |
      | 51        | 5          |
      | -3        | 5          |
      | 12        | 0          |
      | 12        | 13         |

  @pending-remediation
  Scenario Outline: Malformed or out-of-range CLI seed arguments are rejected explicitly
    Given EuroPredict.UI.SimpleConsole is invoked with seed argument "<cli_arg>"
    When Program.Main parses the CLI seed tokens
    Then the application must reject the invalid seed argument with a clear validation message
    And it must not silently execute an unconstrained prediction

    Examples:
      | cli_arg       |
      | abc,12        |
      | 0,55          |
      | 1,2,3,4,5,6   |
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial security requirement mitigating domain/CLI input tampering | RETRO-INCEPTION-001 |
