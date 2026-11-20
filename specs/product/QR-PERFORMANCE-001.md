---
id: QR-PERFORMANCE-001
type: requirement
title: In-Memory Frequency Aggregation Efficiency
status: draft
version: "1.0.0"
schema-version: "1.0"
category: quality
derives-from:
  - UC-PREDICT-COMMON-001
  - UC-PREDICT-FILTERED-001
verifiable-by: automated-unit-test
acceptance-format: declarative-prose
supersedes: null
superseded-by: null
---

# QR-PERFORMANCE-001: In-Memory Frequency Aggregation Efficiency

## 1. Normative Statement
The calculation engine MUST perform frequency aggregation using single-pass LINQ grouping (`GroupBy`) in `CombinationsSummaryAdapter` and set-based superset checks (`HashSet<int>.IsSupersetOf`) in `CombinationsCalculator.IsContained`, avoiding nested quadratic scans over number arrays when filtering historical draw collections.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-PREDICT-COMMON-001` | Derived from Use Case | Validated |
| **Product (Upstream)** | `UC-PREDICT-FILTERED-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria (Declarative)
- `CombinationsSummaryAdapter.SummarizeCollection` aggregates occurrences in O(N) time using `GroupBy`.
- `CombinationsCalculator.IsContained` uses `HashSet<int>.IsSupersetOf` for O(1) average-case membership lookup per seed element.
- Unit tests in `Europredict.Core.CombinationCalculatorTests` execute over representative datasets without timeout.

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is quality requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
