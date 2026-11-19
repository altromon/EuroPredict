---
id: UC-PREDICT-FILTERED-001
type: use-case
title: Generate Conditional EuroMillions Combination from Seed Numbers
status: draft
version: "1.0.0"
schema-version: "1.0"
primary-actor: ACT-ANALYST-001
supporting-actors: []
supersedes: null
superseded-by: null
---

# UC-PREDICT-FILTERED-001: Generate Conditional EuroMillions Combination from Seed Numbers

## 1. Intention and Outcome (User Story)

- **As a**: `ACT-ANALYST-001` (Console User / Analyst)
- **I want**: To pass comma-separated seed Columns (`args[0]`) and optional seed Stars (`args[1]`) to `EuroPredict.UI.SimpleConsole`
- **To**: Receive the most frequent 5 Columns and 2 Stars computed exclusively from historical draws that contain all of my seed numbers and stars.

## 2. Preconditions
- Historical combinations have been loaded via `UC-LOAD-COMBINATIONS-001`.
- At least one command-line argument is provided (`args.Count() >= 1`).

## 3. Main Success Scenario
1. `Program.Main` parses `args[0]` via `ConvertToIntCollection` into seed `columns` (and `args[1]` into seed `stars` if `args.Count() > 1`).
2. `CombinationsCalculator.GetMostProbableCombinationFromNumber(combinations, columns, stars)` filters `combinations` to those where `combination.Columns` is a superset of `columns` and `combination.Stars` is a superset of `stars` (using `HashSet<int>.IsSupersetOf`, treating null/empty seed collections as unconstrained).
3. `GetCombinationWithMostCommonNumbers` summarizes the filtered subset, ranks Columns and Stars descending by `TotalTimes`, and returns the top 5 Columns and top 2 Stars.
4. `Program.Main` prints `Columns: <sorted>` and `Stars: <sorted>` via `ToSortedString()`.

## 4. Alternative Scenarios and Exceptions
- **4.1 No Matching Historical Combinations**: When no historical combination satisfies the seed filter (`combination.Columns.Count() == 0`), `Program.Main` prints `Resources.NoCombination` (`No Combination matches the provided requirements - Columns: {0}, Stars: {1}`).
- **4.2 Non-Numeric Seed Argument**: `ConvertToIntCollection` catches `FormatException` and returns an empty `List<int>`, which `IsContained` treats as unconstrained (`true`).

---

## 5. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is use case from brownfield codebase inspection | RETRO-INCEPTION-001 |
