# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/security/SEC-REQ-DOS-001.md
# ID Requerimiento: SEC-REQ-DOS-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

@SEC-REQ-DOS-001 @dos-protection
Feature: Bounded Payload Size and Timeout Enforcement
  As a Security Officer
  I want EuroPredict to bound download sizes, timeouts, and workbook parsing
  So that oversized or malformed CSV/Excel payloads cannot exhaust memory or hang the process

  @pending-remediation
  Scenario: Oversized CSV or Excel payload is rejected before full memory buffering
    Given a DataSource returning a stream larger than the configured maximum byte limit
    When GetCombinations is invoked
    Then ingestion must abort immediately upon exceeding the byte limit
    And an InvalidOperationException must be raised without exhausting process memory

  @pending-remediation
  Scenario: Stalled remote HTTP endpoint triggers deterministic timeout
    Given an HTTPS DataSource that stalls without sending response bytes
    When GetCombinations is invoked
    Then the HTTP client must cancel the request after the configured timeout
    And no indefinite process hang may occur

  @pending-remediation
  Scenario: Corrupted Excel archive fails safely without unhandled parser crash
    Given an Excel DataSource containing truncated or malformed OOXML/BIFF bytes
    When GetCombinations is invoked
    Then the repository must fail cleanly with a controlled exception or empty safe result
    And no sensitive stack traces may be exposed to standard output
