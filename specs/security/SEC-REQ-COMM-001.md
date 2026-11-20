---
id: SEC-REQ-COMM-001
type: security-requirement
title: Enforce Encrypted HTTPS Transport for Remote Data Sources
status: draft
version: "1.0.0"
schema-version: "1.0"
security-domain: communication-security
mitigates-abuse-case:
  - ABUSE-MITM-HTTP-001
enforced-in-enclave: SEC-ENC-INGEST-001
compliance-references:
  - "OWASP-ASVS-V9.1.1"
  - "OWASP-ASVS-V9.1.2"
acceptance-format: gherkin
cucumber-tags:
  - "@SEC-REQ-COMM-001"
  - "@transport-security"
supersedes: null
superseded-by: null
---

# SEC-REQ-COMM-001: Enforce Encrypted HTTPS Transport for Remote Data Sources

## 1. Technical Control Statement
> **Target / To-Be Security Remediation Requirement**: Not yet implemented in the As-Is legacy codebase (pending a future `CHG-*` hardening increment).

The system MUST reject unencrypted `http://` (and non-HTTPS remote schemes such as `ftp://`) in `DataSource` configuration within `SEC-ENC-INGEST-001`, requiring TLS 1.2+ (`https://`) with valid server certificate chain verification for all remote CSV and Excel downloads (allowing local filesystem paths only when explicitly resolved as local files).

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product / Threat (Upstream)** | `ABUSE-MITM-HTTP-001` | Mitigates Abuse Case | Validated |
| **Enclave** | `SEC-ENC-INGEST-001` | Enforced in Enclave | Validated |

---

## 3. Mitigation Criteria in Gherkin Format (Cucumber Security Tests)

```gherkin
@SEC-REQ-COMM-001 @transport-security
Feature: Encrypted Transport Enforcement for Remote Data Sources
  As a Security Officer
  I want EuroPredict to reject cleartext HTTP and untrusted TLS connections
  So that remote lottery feeds cannot be tampered with or spoofed in transit

  @pending-remediation
  Scenario Outline: Unencrypted or disallowed remote URI schemes are rejected
    Given a combination repository configured with DataSource "<datasource_uri>"
    When GetCombinations is invoked
    Then the repository must reject the request before initiating a network download
    And an ArgumentException or SecurityException must be raised indicating insecure transport

    Examples:
      | datasource_uri                                                                                 |
      | http://www.loteriasyapuestas.es/f/loterias/documentos/Euromillones/HistoricoEuromillones.xls   |
      | ftp://example.com/euromillones.csv                                                             |

  @pending-remediation
  Scenario: Untrusted TLS certificate is rejected during HTTPS download
    Given a combination repository configured with an HTTPS DataSource presenting an invalid or self-signed certificate
    When GetCombinations is invoked
    Then the TLS handshake must fail and abort the download
    And no unverified payload bytes may be passed to the parser
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial security requirement mitigating cleartext HTTP MITM | RETRO-INCEPTION-001 |
