# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/product/FR-INGEST-EXCEL-001.md
# ID Requerimiento: FR-INGEST-EXCEL-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

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
