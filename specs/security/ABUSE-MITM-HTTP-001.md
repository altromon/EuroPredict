---
id: ABUSE-MITM-HTTP-001
type: abuse-case
title: MITM Tampering and Spoofing of Unencrypted HTTP Excel Feed
status: draft
version: "1.0.0"
schema-version: "1.0"
primary-threat-actor: ACT-THREAT-MITM-001
targets-use-case: UC-LOAD-COMBINATIONS-001
stride-category: tampering
mitigated-by:
  - SEC-REQ-COMM-001
asvs-controls:
  - "V9.1.1"
  - "V9.1.2"
supersedes: null
superseded-by: null
---

# ABUSE-MITM-HTTP-001: MITM Tampering and Spoofing of Unencrypted HTTP Excel Feed

## 1. Attack Vector and Exploitation Mechanics
1. `EuroPredict.UI.SimpleConsole` reads `CombinationsDatasource` from `appsettings.json` (which contains a legacy cleartext `http://www.loteriasyapuestas.es/.../HistoricoEuromillones.xls` endpoint).
2. `ExcelCombinationRepository` (or `CSVCombinationRepository`) invokes `WebClient.DownloadData(DataSource)` over unencrypted HTTP.
3. `ACT-THREAT-MITM-001` intercepts the HTTP request on the local network or transit path and serves a spoofed `.xls`/`.xlsx` or `.csv` response with altered historical draw numbers or a weaponized binary payload.
4. `EuroPredict` ingests the tampered dataset without transport integrity verification and outputs manipulated combination recommendations to `ACT-ANALYST-001`.

## 2. Attack Preconditions
- `DataSource` is configured with a cleartext `http://` URI instead of `https://`.
- Adversary has on-path network interception capability (e.g., public Wi-Fi, ARP spoofing, rogue DNS/proxy).

## 3. Potential Impact
- **Integrity**: Silent corruption of historical draw frequencies and predicted combinations.
- **Availability**: Delivery of a malformed `.xls` payload causing `ExcelDataReader` failures or memory exhaustion.

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial As-Is abuse case for cleartext HTTP feed tampering | RETRO-INCEPTION-001 |
