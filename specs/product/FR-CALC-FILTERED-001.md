---
id: FR-CALC-FILTERED-001
type: requirement
title: Conditional Seed-Filtered Combination Prediction and Empty-Match Handling
status: draft
version: "1.0.0"
schema-version: "1.0"
category: functional
derives-from:
  - UC-PREDICT-FILTERED-001
verifiable-by: cucumber-bdd
acceptance-format: gherkin
cucumber-tags:
  - "@FR-CALC-FILTERED-001"
  - "@automated"
  - "@regression"
supersedes: null
superseded-by: null
---

# FR-CALC-FILTERED-001: Conditional Seed-Filtered Combination Prediction and Empty-Match Handling

## 1. Normative Statement
When `EuroPredict.UI.SimpleConsole` is invoked with 1 or 2 command-line arguments (`args[0]` for comma-separated seed Columns, optional `args[1]` for comma-separated seed Stars), `CombinationsCalculator.GetMostProbableCombinationFromNumber` MUST filter historical combinations to those whose `Columns` and `Stars` are supersets of the provided seeds, compute the top 5 Columns and top 2 Stars from the matching subset, and either print them sorted ascending or print `No Combination matches the provided requirements - Columns: {0}, Stars: {1}` when no combinations match.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-PREDICT-FILTERED-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria in Gherkin (Cucumber) Format

```gherkin
@FR-CALC-FILTERED-001 @automated @regression
Feature: Conditional Seed-Filtered Combination Prediction and Empty-Match Handling
  As an ACT-ANALYST-001
  I want to filter historical draws by seed Columns and optional seed Stars
  So that I receive the most frequent numbers co-occurring with my seeds or a clear no-match message

  Background:
    Given a historical dataset of EuroMillions combinations loaded into memory

  Scenario: Nominal conditional prediction with seed Columns and Stars
    Given historical draws containing Columns [12, 17, 22, 33, 39] with Stars [2, 12]
    When EuroPredict.UI.SimpleConsole is invoked with args "12,22" and "2"
    Then standard output contains sorted "Columns: " including 12 and 22
    And standard output contains sorted "Stars: " including 2

  Scenario Outline: Conditional filtering with matching and non-matching seeds
    Given seed Columns "<seed_columns>" and seed Stars "<seed_stars>"
    When GetMostProbableCombinationFromNumber filters the historical dataset
    Then the console outputs "<expected_output_prefix>"

    Examples:
      | seed_columns | seed_stars | expected_output_prefix                                  |
      | 12           |            | Columns:                                                |
      | 12,17        | 2          | Columns:                                                |
      | 99,100       | 99         | No Combination matches the provided requirements -      |
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is functional requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
