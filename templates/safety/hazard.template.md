---
id: HAZ-NAME-001
type: hazard
title: Descriptive Operational Hazard Title
status: draft
version: "1.0.0"
schema-version: "1.0"
severity: critical # catastrophic, critical, major, minor, negligible
probability: remote # frequent, probable, occasional, remote, improbable, extremely-improbable
fault-tolerance-time-ms: 250 # FTTI: Fault tolerance time interval in milliseconds

# Safety requirements mitigating this hazard (Downstream)
mitigated-by:
  - SAF-REQ-NAME-001

hazardous-condition: "Description of the anomalous operational condition or failure generating the hazard."
potential-effect: "Loss of operational control, structural damage, or unintended collision."
supersedes: null
superseded-by: null
---

# HAZ-NAME-001: Descriptive Operational Hazard Title

## 1. Operational Hazard Definition
Detailed and concise description of the unintended operational condition representing a risk to the mission, the system, or the physical environment.

## 2. Severity and Probability Classification

| Parameter | Classification | Technical Justification |
| :--- | :--- | :--- |
| **Severity** | `critical` | Potential significant damage to aircraft or mission without direct human casualty. |
| **Probability** | `remote` | Estimated failure rate $< 10^{-5}$ per nominal operating hour. |
| **FTTI (Fault Tolerance)** | `250 ms` | Maximum time window to transition to safe state prior to adverse effects. |

## 3. Triggering Conditions and Effects

### Anomalous Condition
Detail the initiating event (sensor failure, link loss, CAN bus freeze, or actuator degradation).

### Potential Effect
Direct consequence if the failure is not detected and mitigated within the FTTI interval.

---

## 4. Mitigation Traceability (Downstream Safety Requirements)

| Safety Requirement | Fail-Safe Action | Mitigation Status |
| :--- | :--- | :--- |
| `SAF-REQ-NAME-001` | Contingency maneuver or transition to safe state | Designed |

---

## 5. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-23 | Safety Engineer | Initial hazard definition creation | CHG-INIT-001 |
