---
id: ACT-THREAT-MITM-001
type: threat-actor
title: Network Man-in-the-Middle (MITM) Attacker
status: draft
version: "1.0.0"
schema-version: "1.0"
threat-capability: supply-chain-attacker
motivation: sabotage
attack-surfaces:
  - "Unencrypted HTTP Excel download (http://www.loteriasyapuestas.es/.../HistoricoEuromillones.xls)"
  - "DNS resolution and cleartext HTTP transport in WebClient.DownloadData"
supersedes: null
superseded-by: null
---

# ACT-THREAT-MITM-001: Network Man-in-the-Middle (MITM) Attacker

## 1. Attacker Characterization
A network-positioned adversary (on-path LAN/Wi-Fi attacker, rogue proxy, or compromised upstream transit) capable of intercepting, spoofing, or modifying cleartext HTTP traffic between `EuroPredict` (`SEC-ENC-INGEST-001`) and remote lottery data feeds (`ACT-DATASOURCE-001`).

## 2. Attack Objectives
- **Integrity Compromise**: Inject fabricated EuroMillions draw results in transit to skew frequency calculations and recommendations.
- **Availability / Parser Exploitation**: Substitute a legitimate `.xls` workbook with a malformed or oversized binary payload to trigger memory exhaustion or parser crashes in `ExcelDataReader`.

---

## 3. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial As-Is MITM threat actor profile | RETRO-INCEPTION-001 |
