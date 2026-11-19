# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/product/FR-CALC-COMMON-001.md
# ID Requerimiento: FR-CALC-COMMON-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

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
