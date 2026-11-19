# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/security/SEC-REQ-INPUT-001.md
# ID Requerimiento: SEC-REQ-INPUT-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

@SEC-REQ-INPUT-001 @input-validation
Feature: EuroMillions Domain and CLI Seed Input Validation
  As a Security Officer
  I want EuroPredict to enforce domain range bounds on draw data and CLI seed arguments
  So that out-of-range numbers or malformed seeds cannot corrupt frequency predictions

  @pending-remediation
  Scenario Outline: Out-of-range draw numbers in data source are rejected
    Given a parsed draw row containing Column "<col_value>" and Star "<star_value>"
    When domain validation evaluates the draw record
    Then the record must be rejected as invalid
    And it must not be included in CombinationsSummaryAdapter frequency counts

    Examples:
      | col_value | star_value |
      | 0         | 5          |
      | 51        | 5          |
      | -3        | 5          |
      | 12        | 0          |
      | 12        | 13         |

  @pending-remediation
  Scenario Outline: Malformed or out-of-range CLI seed arguments are rejected explicitly
    Given EuroPredict.UI.SimpleConsole is invoked with seed argument "<cli_arg>"
    When Program.Main parses the CLI seed tokens
    Then the application must reject the invalid seed argument with a clear validation message
    And it must not silently execute an unconstrained prediction

    Examples:
      | cli_arg       |
      | abc,12        |
      | 0,55          |
      | 1,2,3,4,5,6   |
