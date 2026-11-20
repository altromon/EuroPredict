---
id: PRIV-REQ-MASKING-001
type: privacy-requirement
title: Mandatory Pseudonymization and Log Redaction of Operator PII
status: draft
version: "1.0.0"
schema-version: "1.0"
privacy-principle: data-minimization # data-minimization, purpose-limitation, storage-limitation, accuracy, integrity-and-confidentiality, transparency-and-consent, data-subject-rights-arco, pseudonymization-anonymization
linddun-category: identifiability # linkability, identifiability, non-repudiation, detectability, disclosure-of-information, unawareness, non-compliance
governs-ropa:
  - PRIV-ROPA-USER-AUTH-001
protects-data-assets:
  - DATA-ASSET-USER-PROFILE-001
enforced-in-enclave: SEC-ENC-DMZ-001
compliance-references:
  - "GDPR-Art-25-Privacy-by-Design"
  - "GDPR-Art-32-Pseudonymization"
  - "ISO-27701-A.7.4"
acceptance-format: gherkin
cucumber-tags:
  - "@PRIV-REQ-MASKING-001"
  - "@privacy"
  - "@linddun"
supersedes: null
superseded-by: null
---

# PRIV-REQ-MASKING-001: Mandatory Pseudonymization and Log Redaction of Operator PII

## 1. Technical Privacy Control Statement
The system MUST pseudonymize or redact all `confidential-pii` fields (`email`, `phone_number`) prior to emitting structured application logs, telemetry traces, or external AI prompts, and MUST enforce automated TTL purge after `365d`.

---

## 2. Upstream Privacy Traceability (PDaC)

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Record of Processing (ROPA)** | `PRIV-ROPA-USER-AUTH-001` | Governs Processing Activity | Validated |
| **Protected Data Asset** | `DATA-ASSET-USER-PROFILE-001` | Enforces Field Masking & TTL | Validated |

---

## 3. Privacy Verification Criteria in Gherkin Format (Cucumber Privacy Tests)

```gherkin
@PRIV-REQ-MASKING-001 @privacy @linddun
Feature: Personal Data Pseudonymization and Zero-PII Telemetry Logging
  As a Data Protection Officer (DPO)
  I want all personal identifiers to be redacted or salted-hashed before logging
  So that application logs and telemetry never expose raw personal data

  Scenario: Structured logger redacts PII fields automatically (Nominal Privacy Control)
    Given an authenticated operator session with email "operator@example.com" and phone "+34600112233"
    When the telemetry ingestion service emits an audit log entry
    Then the serialized log payload must not contain the literal string "operator@example.com"
    And the serialized log payload must not contain the literal string "+34600112233"
    And the email field must be replaced by a deterministic SHA-256 salted pseudonym

  Scenario: Right to erasure purges expired or revoked subject records
    Given a personal data record for subject "ACT-USER-001" exceeding the "365d" retention TTL
    When the automated retention enforcement job executes
    Then the personal data fields must be irreversibly erased via crypto-shredding
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-10-17 | Privacy Officer (`agent-privacy-officer`) | Initial privacy control specification | CHG-PRIV-001 |
