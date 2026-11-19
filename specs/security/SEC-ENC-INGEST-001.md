---
id: SEC-ENC-INGEST-001
type: security-enclave
title: External Data Ingestion Enclave
status: draft
version: "1.0.0"
schema-version: "1.0"
trust-zone: untrusted
trust-level: "Untrusted external network and filesystem ingestion boundary"
isolation: "WebClient.DownloadData and format parser boundary (CSVCombinationRepository & ExcelCombinationRepository)"
perimeter-rules:
  - "All remote data sources must use TLS 1.2+ (HTTPS) with valid server certificate verification"
  - "Downloaded payloads must be bounded in size and timeout to prevent memory exhaustion"
  - "URI schemes must be restricted to approved protocols (https or explicit local file path)"
allowed-inbound:
  - SEC-ENC-LOCAL-001
allowed-outbound:
  - SEC-ENC-LOCAL-001
enforced-by:
  - SEC-REQ-COMM-001
  - SEC-REQ-DOS-001
authentication-mechanism: none
supersedes: null
superseded-by: null
---

# SEC-ENC-INGEST-001: External Data Ingestion Enclave

## 1. Enclave Definition and Purpose
The **External Data Ingestion Enclave** encompasses the untrusted boundary where `EuroPredict.Core.CSVCombinationRepository` and `EuroPredict.Core.ExcelCombinationRepository` retrieve raw bytes via `WebClient.DownloadData(DataSource)` from `ACT-DATASOURCE-001` (remote HTTP/HTTPS URLs or local files specified in `appsettings.json`) and parse them into `MemoryStream` / `ExcelDataReader` / CSV structures.

> **As-Is Security Observation**: `EuroPredict.UI.SimpleConsole/appsettings.json` includes a legacy cleartext HTTP URL (`http://www.loteriasyapuestas.es/.../HistoricoEuromillones.xls`), and `WebClient.DownloadData` buffers the entire payload in memory without size or timeout limits.

## 2. Trust Zone and Perimeter Rules

| Parameter | Value | Security Justification |
| :--- | :--- | :--- |
| **Trust Zone** | `untrusted` | External HTTP/HTTPS feeds and third-party Excel/CSV files cannot be implicitly trusted. |
| **Authentication** | `none` | Publicly accessible lottery archives require no client credentials. |

### Perimeter Rules
1. **Transport Encryption**: Remote URLs must use `https://` to prevent MITM tampering and spoofing.
2. **Resource Bounding**: Ingestion must enforce maximum payload size and request timeouts before buffering into `MemoryStream` or invoking `ExcelReaderFactory.CreateReader`.
3. **Parser Isolation**: Malformed CSV lines or corrupted `.xls`/`.xlsx` structures must fail safely without unhandled exceptions or unbounded allocations.

---

## 3. Authorized Connectivity Matrix

| Direction | Entity Identifier | Type | Protocol / Channel |
| :--- | :--- | :--- | :--- |
| **Inbound** | `SEC-ENC-LOCAL-001` | Enclave | `GetCombinations()` invocation with `DataSource` URI/path |
| **Outbound** | `SEC-ENC-LOCAL-001` | Enclave | Validated `IEnumerable<ICombination>` collection |

---

## 4. Linked Security Requirements

- `SEC-REQ-COMM-001`: Enforce encrypted HTTPS transport for remote data sources.
- `SEC-REQ-DOS-001`: Enforce payload size limits and timeouts during data ingestion.

---

## 5. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial As-Is security enclave for external data ingestion | RETRO-INCEPTION-001 |
