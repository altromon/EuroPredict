---
id: FR-CALC-COMMON-001
type: requirement
title: Unconditioned Most-Common Combination Calculation and Console Output
status: draft
version: "1.0.0"
schema-version: "1.0"
category: functional
derives-from:
  - UC-PREDICT-COMMON-001
verifiable-by: cucumber-bdd
acceptance-format: gherkin
cucumber-tags:
  - "@FR-CALC-COMMON-001"
  - "@automated"
  - "@regression"
supersedes: null
superseded-by: null
---

# FR-CALC-COMMON-001: Unconditioned Most-Common Combination Calculation and Console Output

## 1. Normative Statement
When `EuroPredict.UI.SimpleConsole` is invoked with zero command-line arguments, `CombinationsCalculator.GetCombinationWithMostCommonNumbers` MUST aggregate occurrence counts for all `Columns` and `Stars` via `CombinationsSummaryAdapter`, sort each descending by `TotalTimes`, select the top 5 Columns and top 2 Stars, and print them sorted ascending as `Columns: <c1>,<c2>,<c3>,<c4>,<c5>` and `Stars: <s1>,<s2>`.

> **As-Is Edge-Case Limitation (`Program.cs:47`)**: When the loaded dataset yields 0 combinations (`combination.Columns.Count() == 0`) and `args.Length == 0`, `Program.Main` enters the empty-combination branch and accesses `args[0]` to format `Resources.NoCombination`, throwing an `IndexOutOfRangeException`.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-PREDICT-COMMON-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria in Gherkin (Cucumber) Format

```gherkin
@FR-CALC-COMMON-001 @automated @regression
Feature: Unconditioned Most-Common Combination Calculation and Console Output
  As an ACT-ANALYST-001
  I want to run the console app with no arguments
  So that I receive the 5 most frequent Columns and 2 most frequent Stars sorted ascending

  Background:
    Given a historical dataset of EuroMillions combinations loaded into memory

  Scenario: Nominal unconditioned prediction with zero CLI arguments
    Given historical combinations where Columns 23, 19, 44, 50, 5 and Stars 2, 8 have the highest TotalTimes
    When EuroPredict.UI.SimpleConsole is invoked with 0 arguments
    Then standard output contains "Columns: 5,19,23,44,50"
    And standard output contains "Stars: 2,8"

  Scenario Outline: Summary frequency ranking selects top N items
    Given a ColumnsSummary with <distinct_columns> distinct numbers and StarsSummary with <distinct_stars> distinct stars
    When GetCombinationWithMostCommonNumbers is called
    Then the resulting combination has <expected_columns> Columns and <expected_stars> Stars

    Examples:
      | distinct_columns | distinct_stars | expected_columns | expected_stars |
      | 10               | 5              | 5                | 2              |
      | 5                | 2              | 5                | 2              |
      | 3                | 1              | 3                | 1              |
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is functional requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
