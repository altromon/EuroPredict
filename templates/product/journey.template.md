---
id: JRN-NAME-001
type: journey
title: Descriptive User or Operator Journey Title
status: draft
version: "1.0.0"
schema-version: "1.0"
persona: ACT-OPERATOR-001
stages:
  - "Discovery and Planning"
  - "Configuration and Pre-flight"
  - "Mission Execution"
  - "Post-Analysis and Wrap-up"
touchpoints:
  - "Mission planning web portal"
  - "Ground control station (GCS)"
  - "Real-time telemetry dashboard"
  - "Flight report exporter"
related-use-cases:
  - UC-PLAN-MISSION-001
  - UC-MONITOR-FLIGHT-001
pain-points:
  - "Latency when loading offline navigation maps"
  - "Complexity in manual flight plan reconciliation"
supersedes: null
superseded-by: null
---

# JRN-NAME-001: Descriptive User or Operator Journey Title

## 1. Actor / Primary Persona Profile
- **Protagonist Actor**: `ACT-OPERATOR-001`
- **Main Goal**: Complete operational workflow with high efficiency, safety, and real-time visibility.

---

## 2. Journey Stages Map

| Stage | User Goal | Touchpoints | Frictions / Pain Points |
| :--- | :--- | :--- | :--- |
| **1. Discovery and Planning** | Design route and validate operational constraints | Mission planning web portal | Complexity in manual flight plan reconciliation |
| **2. Configuration and Pre-flight** | Verify subsystem health and link integrity | Ground control station (GCS) | Latency when loading offline navigation maps |
| **3. Mission Execution** | Supervise telemetry and respond to alerts | Real-time telemetry dashboard | None detected in nominal flow |
| **4. Post-Analysis and Wrap-up** | Export mission logs and performance metrics | Flight report exporter | Telemetry consolidation latency |

---

## 3. Traceability to Use Cases and Product

- `UC-PLAN-MISSION-001`: Planning and validation of safe corridors.
- `UC-MONITOR-FLIGHT-001`: Ingestion and visualization of real-time telemetry.

---

## 4. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-23 | Product Manager / UX Lead | Initial operator journey definition | CHG-INIT-001 |
