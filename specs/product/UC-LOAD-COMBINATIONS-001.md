---
id: UC-LOAD-COMBINATIONS-001
type: use-case
title: Load Historical EuroMillions Combinations from Data Source
status: draft
version: "1.0.0"
schema-version: "1.0"
primary-actor: ACT-ANALYST-001
supporting-actors:
  - ACT-DATASOURCE-001
supersedes: null
superseded-by: null
---

# UC-LOAD-COMBINATIONS-001: Load Historical EuroMillions Combinations from Data Source

## 1. Intention and Outcome (User Story)

- **As a**: `ACT-ANALYST-001` (Console User / Analyst)
- **I want**: The system to retrieve and parse historical EuroMillions draw combinations from the configured CSV or Excel data source (`ACT-DATASOURCE-001`)
- **To**: Populate an in-memory collection of `ICombination` records (each containing 5 Columns and 2 Stars) ready for frequency analysis.

## 2. Preconditions
- `appsettings.json` (or repository `DataSource` property) specifies a valid, non-empty file path or HTTP/HTTPS URL.
- For CSV ingestion, `CombinationRepository.CombinationFactory` is initialized (e.g., `new CombinationFactory()`).

## 3. Main Success Scenario
1. The system reads `CombinationsDatasource` from `appsettings.json` via `ConfigurationBuilder`.
2. The `ICombinationRepository` implementation (`CSVCombinationRepository.CombinationRepository` or `ExcelCombinationRepository.CombinationRepository`) downloads the raw bytes from `DataSource` using `WebClient.DownloadData` into a `MemoryStream`.
3. For CSV sources, `GetCSV()` reads all lines and `CombinationFactory.CreateCombination(line)` splits each line by `,`, parsing indices `1..5` as `Columns` and indices `7..8` as `Stars`.
4. For Excel sources, `GetExcelDataSet()` registers `CodePagesEncodingProvider`, reads the workbook via `ExcelReaderFactory.CreateReader`, and `ExtractSheetCombinations` scans each row for a `DateTime` cell, extracting the next 5 numeric cells as `Columns` and following 2 as `Stars`.
5. The system returns the resulting `IEnumerable<ICombination>` collection.

## 4. Alternative Scenarios and Exceptions
- **4.1 Empty or Whitespace DataSource**: `GetStream()` throws `ArgumentNullException` when `DataSource` is null or whitespace.
- **4.2 Missing CombinationFactory (CSV)**: `ExtractCombinations()` throws `ArgumentNullException` if `CombinationFactory` is null.
- **4.3 Header or Malformed CSV Row**: `CombinationFactory.CreateCombination` catches parsing exceptions, returns `null`, and the repository skips the row.
- **4.4 Empty/Partial Excel Row**: `ExtractSheetCombinations` filters out any combination where `Columns.Count() == 0` or `Stars.Count() == 0`.

---

## 5. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is use case from brownfield codebase inspection | RETRO-INCEPTION-001 |
