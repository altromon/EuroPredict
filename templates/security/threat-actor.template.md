---
id: ACT-THREAT-NAME-001
type: threat-actor
title: Adversary or Threat Profile
status: draft
version: "1.0.0"
schema-version: "1.0"
threat-capability: advanced-persistent-threat # script-kiddie, advanced-persistent-threat, insider-threat, automated-botnet, supply-chain-attacker
motivation: financial-gain # financial-gain, espionage, sabotage, data-theft, reputational-damage
attack-surfaces:
  - "Public REST API endpoints"
  - "WebSocket connections without mTLS"
supersedes: null
superseded-by: null
---

# ACT-THREAT-NAME-001: Adversary Profile

## 1. Attacker Characterization
Describes the adversary's profile, technical skill level, available resources, and typical modus operandi.

## 2. Attack Objectives
- Which information assets or services the adversary seeks to compromise.
- Estimated impact on confidentiality, integrity, and availability.

---

## 3. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-03 | Security Officer / Threat Modeler | Initial threat profile characterization | CHG-SEC-001 |
