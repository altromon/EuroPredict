# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/product/FR-CALC-FILTERED-001.md
# ID Requerimiento: FR-CALC-FILTERED-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

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
