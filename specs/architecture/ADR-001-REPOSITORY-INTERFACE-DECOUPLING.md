---
id: ADR-001-REPOSITORY-INTERFACE-DECOUPLING
type: architecture-decision-record
title: Decouple Calculation Engine from Data Ingestion via EuroPredict.Model.Interfaces
status: accepted
version: "1.0.0"
schema-version: "1.0"
deciders:
  - "EuroPredict Original Author"
  - "System Architect Agent (Retro-Inception)"
decision-date: "2026-11-19"
affects-components:
  - CMP-MODEL-CONTRACTS-001
  - CMP-CALC-ENGINE-001
  - CMP-DATA-REPOSITORIES-001
  - CMP-CLI-CONSOLE-001
supersedes: null
superseded-by: null
---

# ADR-001: Decouple Calculation Engine from Data Ingestion via EuroPredict.Model.Interfaces

## 1. Context and Problem Statement
`EuroPredict` must support multiple historical draw data formats (CSV and Excel `.xls`/`.xlsx` from local files or remote URLs) while keeping the statistical frequency calculation engine (`QR-MODULARITY-001`) independent of file-parsing libraries and I/O concerns.

## 2. Considered Technology Options
1. **Option A — Shared Interface Contract Assembly (`EuroPredict.Model.Interfaces`)**: Isolate `ICombinationRepository`, `ICombination`, `ICombinationsSummary`, and `INumberInfo` in a dependency-free `netstandard1.0` assembly referenced by both the calculation engine and repository adapters.
2. **Option B — Direct Coupling in Calculation Engine**: Embed CSV/Excel parsing directly inside `CombinationCalculator`.

## 3. Decision Outcome
Chosen **Option A** (observed As-Is in `EuroPredict.Model.Interfaces`): `Europredict.Core.CombinationCalculator` references only `EuroPredict.Model.Interfaces`, while `CSVCombinationRepository` and `ExcelCombinationRepository` independently implement `ICombinationRepository`.

## 4. Consequences
- **Positive**: Calculation logic is fully unit-testable in isolation and agnostic to data source formats (`QR-MODULARITY-001`).
- **Negative / Trade-offs**: `EuroPredict.UI.SimpleConsole/Program.cs` currently wires the concrete `CSVCombinationRepository.CombinationRepository` directly via `using` (with `ExcelCombinationRepository` commented out), rather than resolving `ICombinationRepository` via configuration or DI.

---

## 5. Revision History and Version Control

| Version | Date | Deciders | Decision Status | Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | System Architect Agent | Retro-inception baseline of As-Is design | RETRO-INCEPTION-001 |
