---
id: FR-INGEST-CSV-001
type: requirement
title: CSV Historical Combination Ingestion
status: draft
version: "1.0.0"
schema-version: "1.0"
category: functional
derives-from:
  - UC-LOAD-COMBINATIONS-001
verifiable-by: cucumber-bdd
acceptance-format: gherkin
cucumber-tags:
  - "@FR-INGEST-CSV-001"
  - "@automated"
  - "@regression"
supersedes: null
superseded-by: null
---

# FR-INGEST-CSV-001: CSV Historical Combination Ingestion

## 1. Normative Statement
The `EuroPredict.Core.CSVCombinationRepository.CombinationRepository` MUST download the CSV resource specified by `DataSource` (via `WebClient.DownloadData`), parse each line using `ICombinationFactory.CreateCombination` (extracting comma-separated indices `1..5` as `Columns` and indices `7..8` as `Stars`), and omit any line for which the factory returns `null`.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-LOAD-COMBINATIONS-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria in Gherkin (Cucumber) Format

```gherkin
@FR-INGEST-CSV-001 @automated @regression
Feature: CSV Historical Combination Ingestion
  As an ACT-ANALYST-001
  I want EuroPredict to parse historical EuroMillions draws from a CSV source
  So that valid draw combinations are loaded while header or malformed rows are skipped

  Background:
    Given a CSVCombinationRepository configured with a valid CombinationFactory

  Scenario: Nominal ingestion of a CSV file with header and valid draw rows
    Given a CSV data source containing header "FECHA,COMB. GANADORA,,,,,,ESTRELLAS," and row "22/10/2021,12,17,22,33,39,,02,12"
    When GetCombinations is invoked
    Then the repository returns 1 combination
    And the combination has Columns [12, 17, 22, 33, 39] and Stars [2, 12]

  Scenario Outline: CSV row parsing and error resilience
    Given a CSV line "<csv_line>"
    When CombinationFactory.CreateCombination parses the line
    Then the parsed result is "<expected_outcome>"

    Examples:
      | csv_line                             | expected_outcome |
      | 19/10/2021,04,20,36,40,41,,01,06     | valid            |
      | FECHA,COMB. GANADORA,,,,,,ESTRELLAS, | null             |
      | invalid,row                          | null             |
```

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is functional requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
