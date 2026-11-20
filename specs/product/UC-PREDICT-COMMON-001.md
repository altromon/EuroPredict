---
id: UC-PREDICT-COMMON-001
type: use-case
title: Generate Most Frequent EuroMillions Combination
status: draft
version: "1.0.0"
schema-version: "1.0"
primary-actor: ACT-ANALYST-001
supporting-actors: []
supersedes: null
superseded-by: null
---

# UC-PREDICT-COMMON-001: Generate Most Frequent EuroMillions Combination

## 1. Intention and Outcome (User Story)

- **As a**: `ACT-ANALYST-001` (Console User / Analyst)
- **I want**: To run `EuroPredict.UI.SimpleConsole` without command-line arguments
- **To**: Receive the 5 most frequently occurring Columns and 2 most frequently occurring Stars across the entire historical dataset, sorted in ascending order.

## 2. Preconditions
- Historical combinations have been loaded via `UC-LOAD-COMBINATIONS-001`.
- Zero command-line arguments are passed (`args.Count() == 0`).

## 3. Main Success Scenario
1. `Program.Main` detects `args.Count() == 0` and invokes `CombinationsCalculator.GetCombinationWithMostCommonNumbers(combinations)`.
2. `CombinationsSummaryAdapter.GetCombinationsSummary()` flattens all `Columns` and `Stars` across the historical dataset and groups each number to count its `TotalTimes` (`NumberInfo`).
3. `CombinationsCalculator.SortMostProbableNumberInfos` orders `ColumnsSummary` and `StarsSummary` descending by `TotalTimes`.
4. `GetCombinationWithMostCommonNumbers` selects the top 5 Columns and top 2 Stars into a `Combination`.
5. `Program.Main` formats both collections using `IEnumerableExtensions.ToSortedString()` (ascending comma-separated) and prints `Columns: <sorted>` and `Stars: <sorted>` to the console.

## 4. Alternative Scenarios and Exceptions
- **4.1 Empty Historical Dataset**: If `combinations` is empty, `Columns` and `Stars` are empty (`combination.Columns.Count() == 0`). In `Program.Main`, accessing `args[0]` when `args.Length == 0` would throw `IndexOutOfRangeException` (observed As-Is behavior).

---

## 5. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is use case from brownfield codebase inspection | RETRO-INCEPTION-001 |
