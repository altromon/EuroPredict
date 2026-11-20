# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/product/FR-INGEST-CSV-001.md
# ID Requerimiento: FR-INGEST-CSV-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

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
