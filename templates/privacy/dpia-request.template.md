---
id: DPIA-ASSESSMENT-001
type: dpia-request
title: Data Protection Impact Assessment (DPIA / EIPD) Request
status: pending-dpo-approval # pending-dpo-approval, approved, rejected
version: "1.0.0"
schema-version: "1.0"
ropa-reference: PRIV-ROPA-USER-AUTH-001
data-assets:
  - DATA-ASSET-USER-PROFILE-001
special-category-triggers:
  - "GDPR Art. 9 Special Category Data or Automated Profiling"
residual-risk-level: MEDIUM # LOW, MEDIUM, HIGH
---

# Data Protection Impact Assessment (DPIA / EIPD — GDPR Art. 35)

## 1. Trigger & Necessity Assessment
- **Associated ROPA**: `PRIV-ROPA-USER-AUTH-001`
- **Data Assets Affected**: `DATA-ASSET-USER-PROFILE-001`
- **Why a DPIA is Mandatory**: Processing involves `special_category_sensitive` data (biometrics, health, precise geolocation) or systematic automated profiling under GDPR Art. 35.

## 2. Necessity, Proportionality & LINDDUN Risk Matrix

| LINDDUN Threat Category | Identified Privacy Risk | Intrinsic Risk | Technical Mitigation (`PRIV-REQ-*`) | Residual Risk |
| :--- | :--- | :---: | :--- | :---: |
| **Identifiability** | Re-identification of operator from telemetry streams | HIGH | `PRIV-REQ-MASKING-001` (SHA-256 salted pseudonymization) | LOW |
| **Disclosure of Information** | Plaintext PII leakage in error logs | HIGH | `PRIV-REQ-MASKING-001` (Redaction middleware) | LOW |

## 3. Mandatory Human Sovereign Signatures (DPO & Legal)
- [ ] **Data Protection Officer (DPO)**: Necessity, proportionality, and GDPR Art. 35 validation
- [ ] **Legal / Privacy Counsel**: Lawful basis and cross-border transfer clauses verified
- [ ] **Security Officer (CISO) / System Architect**: Cryptographic and enclave controls verified
