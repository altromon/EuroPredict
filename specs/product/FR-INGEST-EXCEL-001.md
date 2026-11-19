---
id: FR-INGEST-EXCEL-001
type: requirement
title: Excel Historical Combination Ingestion
status: draft
version: "1.0.0"
schema-version: "1.0"
category: functional
derives-from:
  - UC-LOAD-COMBINATIONS-001
verifiable-by: cucumber-bdd
acceptance-format: gherkin
cucumber-tags:
  - "@FR-INGEST-EXCEL-001"
  - "@automated"
  - "@regression"
supersedes: null
superseded-by: null
---

# FR-INGEST-EXCEL-001: Excel Historical Combination Ingestion

## 1. Normative Statement
The `EuroPredict.Core.ExcelCombinationRepository.CombinationRepository` MUST read the Excel workbook specified by `DataSource` via `ExcelDataReader`, scan every worksheet row for a `DateTime` cell, extract the 5 subsequent cells as `Columns` and the following 2 cells as `Stars` (ignoring `DBNull` and non-numeric strings), and return only combinations with at least one Column and at least one Star.

> **As-Is `CombinationBuilder` Edge-Case Limitations**:
> - **No Bounds Check**: `CombinationBuilder.CreateCombination` / `ReadColumns` does not verify `itemArray.Length` before indexing `preInitialIndex + i`; if a `DateTime` cell appears within the last 7 columns of a row, an `IndexOutOfRangeException` is thrown.
> - **Partial-Match Numeric Parsing**: `CombinationBuilder.ReadColumns` filters string cells with `Regex.IsMatch(item as string, "[0-9]+")` and then calls `Convert.ToInt32(item)`, which throws `FormatException`, `OverflowException`, or `InvalidCastException` on malformed non-pure-integer cells (e.g., `"12a"` or overflowing values).

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-LOAD-COMBINATIONS-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria in Gherkin (Cucumber) Format

```gherkin
@FR-INGEST-EXCEL-001 @automated @regression
Feature: Excel Historical Combination Ingestion
  As an ACT-ANALYST-001
  I want EuroPredict to parse historical EuroMillions draws from an Excel workbook
  So that legacy .xls/.xlsx archives can be analyzed identically to CSV feeds

  Background:
    Given an ExcelCombinationRepository with CodePagesEncodingProvider registered

  Scenario: Nominal ingestion of an Excel sheet with DateTime draw rows
    Given an Excel worksheet containing a title row and a draw row starting with DateTime "2021-10-22" followed by 12, 17, 22, 33, 39, 2, 12
    When GetCombinations is invoked
    Then the repository returns 1 combination
    And the combination has Columns [12, 17, 22, 33, 39] and Stars [2, 12]

  Scenario Outline: Excel row filtering and cell parsing
    Given a worksheet row with first cell "<first_cell_type>" and numeric cells "<cell_values>"
    When ExtractSheetCombinations processes the row
    Then the row is "<inclusion_result>" in the returned combinations

    Examples:
      | first_cell_type | cell_values          | inclusion_result |
      | DateTime        | 12,17,22,33,39,2,12  | included         |
      | String          | 12,17,22,33,39,2,12  | excluded         |
      | DateTime        | DBNull               | excluded         |
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is functional requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
