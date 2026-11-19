---
id: DATA-ASSET-USER-PROFILE-001
type: data-asset
title: User Profile & Identity Personal Data Asset
status: draft
version: "1.0.0"
schema-version: "1.0"
classification: confidential-pii # public, internal, confidential-pii, special-category-sensitive
pii-categories:
  - identifiers
  - contact
data-subjects:
  - ACT-USER-001
fields:
  - name: email
    sensitivity: pii
    masking-strategy: sha256-salt
  - name: phone_number
    sensitivity: pii
    masking-strategy: redact
  - name: operator_callsign
    sensitivity: internal
    masking-strategy: none
retention-period: 365d
erasure-method: crypto-shredding
owner-bounded-context: CMP-IDENTITY-SERVICE-001
supersedes: null
superseded-by: null
---

# DATA-ASSET-USER-PROFILE-001: User Profile & Identity Personal Data Asset

## 1. Data Asset Inventory & Purpose
Describes the structure, storage medium, and operational boundary of the personal data asset governed under GDPR / ISO 27701.

## 2. Field-Level Sensitivity & Masking Policy

| Field Name | Sensitivity | Masking / Pseudonymization Strategy | Allowed in Logs | Allowed in AI Prompts |
| :--- | :--- | :--- | :---: | :---: |
| `email` | `pii` | `sha256-salt` | ❌ No | ❌ No |
| `phone_number` | `pii` | `redact` | ❌ No | ❌ No |
| `operator_callsign` | `internal` | `none` | ✅ Yes | ✅ Yes |

## 3. Storage Limitation & Right to Erasure (GDPR Art. 17)
- **Maximum Retention TTL**: `365d` after account deactivation.
- **Erasure Mechanism**: `crypto-shredding` (destruction of per-subject encryption key and PII column purge).

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-10-17 | Privacy Officer (`agent-privacy-officer`) | Initial personal data asset inventory | CHG-PRIV-001 |
