---
id: QR-MODULARITY-001
type: requirement
title: Pluggable Repository and Domain Model Abstraction
status: draft
version: "1.0.0"
schema-version: "1.0"
category: quality
derives-from:
  - UC-LOAD-COMBINATIONS-001
  - UC-PREDICT-COMMON-001
verifiable-by: inspection
acceptance-format: declarative-prose
supersedes: null
superseded-by: null
---

# QR-MODULARITY-001: Pluggable Repository and Domain Model Abstraction

## 1. Normative Statement
The `Europredict.Core.CombinationCalculator` assembly MUST depend solely on `EuroPredict.Model.Interfaces` (`ICombination`, `ICombinationsSummary`, `INumberInfo`) and MUST NOT reference any concrete data-source repository (`CSVCombinationRepository` or `ExcelCombinationRepository`) or UI assembly. Repository implementations MUST implement `ICombinationRepository` so data sources can be interchanged independently.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-LOAD-COMBINATIONS-001` | Derived from Use Case | Validated |
| **Product (Upstream)** | `UC-PREDICT-COMMON-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria (Declarative)
- `Europredict.Core.CombinationCalculator.csproj` has project references only to `EuroPredict.Model.Interfaces`.
- Both `EuroPredict.Core.CSVCombinationRepository.CombinationRepository` and `EuroPredict.Core.ExcelCombinationRepository.CombinationRepository` implement `EuroPredict.Model.Interfaces.ICombinationRepository`.
- `CombinationsCalculator` operates exclusively on `IEnumerable<ICombination>` and `ICombinationsSummary`.

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is quality requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
