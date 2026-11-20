---
id: ARCH-INTRO-001
type: architecture-introduction
title: "01. Introduction and System Goals"
status: draft
version: "1.0.0"
schema-version: "1.0"
arc42-section: 1
naf-perspective: "Enterprise & Capability"
cites-product-actors:
  - ACT-ANALYST-001
  - ACT-DATASOURCE-001
cites-quality-goals:
  - QR-MODULARITY-001
  - QR-PERFORMANCE-001
  - QR-RELIABILITY-001
cites-journeys:
  - JRN-PREDICT-001
supersedes: null
superseded-by: null
---

# 01. Introduction and System Goals (arc42 Sec. 1 / NAF Enterprise)

## 1. System Vision and Executive Summary
`EuroPredict` is a .NET Core console application and modular statistical calculation library designed to ingest historical EuroMillions lottery draw results (5 main numbers known as **Columns** and 2 bonus numbers known as **Stars**), compute historical occurrence frequencies, and recommend the most frequent combination—either across the entire historical dataset or conditioned on user-specified seed numbers and stars.

### 1.1 Mission Statement
> *The EuroPredict system provides historical EuroMillions frequency analysis and combination recommendation for console analysts (`ACT-ANALYST-001`), guaranteeing decoupled CSV/Excel data ingestion (`ACT-DATASOURCE-001`), fast in-memory aggregation, and resilient parsing of historical draw records.*

### 1.2 Interaction Modality (As-Is)
- **Primary Entry Point**: Command-line interface (`EuroPredict.UI.SimpleConsole`) reading `CombinationsDatasource` from `appsettings.json` and accepting optional positional CLI arguments (`args[0]` for comma-separated seed Columns, `args[1]` for comma-separated seed Stars).
- **Output**: Plain-text standard output displaying sorted `Columns:` and `Stars:`, or a localized resource message (`Resources.NoCombination`) when no historical combinations match the requested seed filter.
- **As-Is Ranking Determinism Note**: `CombinationsCalculator.SortMostProbableNumberInfos` sorts exclusively by `OrderByDescending(numberInfo => numberInfo.TotalTimes)` without a secondary tie-breaker; consequently, tie-breaking among equally frequent numbers depends on their input order.

---

## 2. Priority Quality Goals (Canonical ProductShape Citations)

| Priority | Quality Goal | Requirement ID | Architectural Rationale |
| :---: | :--- | :--- | :--- |
| **1** | Pluggable Repository & Domain Abstraction | `QR-MODULARITY-001` | Core calculation logic depends solely on `EuroPredict.Model.Interfaces` (`ICombinationRepository`, `ICombination`, `ICombinationsSummary`, `INumberInfo`), allowing CSV and Excel repositories to be swapped without modifying `CombinationsCalculator`. |
| **2** | In-Memory Frequency Aggregation Efficiency | `QR-PERFORMANCE-001` | Uses LINQ `GroupBy` summarization (`CombinationsSummaryAdapter`) and `HashSet<int>.IsSupersetOf` filtering (`CombinationsCalculator`) for sub-second analysis over thousands of historical draws. |
| **3** | Resilient Malformed-Row & Input Handling | `QR-RELIABILITY-001` | Skips CSV header/unparseable rows (`CombinationFactory`), ignores `DBNull`/non-numeric Excel cells (`CombinationBuilder`), and safely defaults invalid CLI token inputs (`Program.ConvertToIntCollection`). |

---

## 3. Stakeholder and Primary Actors Matrix

| Role / Stakeholder | Actor ID | Architectural Expectations |
| :--- | :--- | :--- |
| **Console User / Analyst** | `ACT-ANALYST-001` | Fast CLI execution, deterministic frequency-based combination output sorted in ascending order, and clear feedback when seed filters yield no matches. |
| **External Lottery Data Source** | `ACT-DATASOURCE-001` | Provides historical EuroMillions draw records in CSV or Excel (`.xls`/`.xlsx`) format via local file path or HTTP/HTTPS URL configured in `appsettings.json`. |

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Brownfield retro-inception baseline capturing As-Is EuroPredict vision, actors, and quality goals | RETRO-INCEPTION-001 |
