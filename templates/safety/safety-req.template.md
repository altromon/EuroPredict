---
# Valid prefixes: SAF-REQ- (Safety Requirement / Operational Safety)
id: SAF-REQ-NAME-001
type: safety-requirement
title: Concise Safety Requirement Title
status: draft
version: "1.0.0"
schema-version: "1.0"
safety-domain: flight-control # flight-control, collision-avoidance, energy-management, emergency-recovery, sensor-integrity
safety-integrity-level: "DAL-B" # DO-178C (DAL-A/B/C/D), ISO 26262 (ASIL-A/B/C/D), IEC 61508 (SIL-1/2/3/4)

# 1. HAZARD AND RISK ANALYSIS TRACEABILITY (Upstream)
mitigates-hazard:
  - HAZ-DESCRIPTION-001

fail-safe-action: "SAFE_STATE_ACTION" # e.g. RETURN_TO_HOME, CONTROLLED_LANDING, MOTOR_CUTOFF
fault-tolerance-time-ms: 100 # Maximum fault tolerance time interval (FTTI) before entering safe state

# 2. VERIFICATION METHOD AND CRITERIA
acceptance-format: gherkin
cucumber-tags:
  - "@SAF-REQ-NAME-001"
  - "@safety"
  - "@failsafe"

supersedes: null
superseded-by: null
---

# SAF-REQ-NAME-001: Concise Safety Requirement Title

## 1. Normative Operational Safety Statement
The system MUST [unambiguous description of defensive behavior preventing a hazard or mitigating unintended loss of control]. If [anomalous condition or hardware/sensor failure], the system MUST transition to safe state `SAFE_STATE_ACTION` within [X] milliseconds.

---

## 2. Hazard Mitigation and Fault Tolerance Matrix

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Hazard Analysis (Upstream)** | `HAZ-DESCRIPTION-001` | Loss / Accident Mitigation | Validated |
| **Integrity Level (SIL/DAL)** | `DAL-B` | Critical Software Level | Compliant |
| **Fail-Safe Action** | `SAFE_STATE_ACTION` | Emergency Transition | Verified |

---

## 3. Mitigation Criteria in Gherkin Format (Safety & Fail-Safe Tests)

```gherkin
@SAF-REQ-NAME-001 @safety @failsafe
Feature: Operational Failure Mitigation and Safe State Transition
  As an Autonomous Flight Control System
  I want to detect critical anomalies and execute contingency maneuvers
  So that I prevent accidents, collisions, and safeguard physical safety

  Scenario: Anomalous condition detection and safe state activation
    Given the system is in nominal operation
    When an unrecoverable failure condition occurs
    Then the system must detect the anomaly in less than 100 milliseconds
    And abort the current trajectory
    And execute contingency maneuver "SAFE_STATE_ACTION"
    And emit a critical alert to the ground control station
```

---

## 4. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-15 | Safety Engineer | Initial safety requirement creation | CHG-INIT-001 |
