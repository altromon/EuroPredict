# ==============================================================================
# AUTO-GENERADO POR AI-SDLC (Cucumber Integration)
# Origen: specs/security/SEC-REQ-COMM-001.md
# ID Requerimiento: SEC-REQ-COMM-001
# Versión: 1.0.0
# NO EDITAR MANUALMENTE: Cualquier cambio debe realizarse en el Markdown origen.
# ==============================================================================

@SEC-REQ-COMM-001 @transport-security
Feature: Encrypted Transport Enforcement for Remote Data Sources
  As a Security Officer
  I want EuroPredict to reject cleartext HTTP and untrusted TLS connections
  So that remote lottery feeds cannot be tampered with or spoofed in transit

  @pending-remediation
  Scenario Outline: Unencrypted or disallowed remote URI schemes are rejected
    Given a combination repository configured with DataSource "<datasource_uri>"
    When GetCombinations is invoked
    Then the repository must reject the request before initiating a network download
    And an ArgumentException or SecurityException must be raised indicating insecure transport

    Examples:
      | datasource_uri                                                                                 |
      | http://www.loteriasyapuestas.es/f/loterias/documentos/Euromillones/HistoricoEuromillones.xls   |
      | ftp://example.com/euromillones.csv                                                             |

  @pending-remediation
  Scenario: Untrusted TLS certificate is rejected during HTTPS download
    Given a combination repository configured with an HTTPS DataSource presenting an invalid or self-signed certificate
    When GetCombinations is invoked
    Then the TLS handshake must fail and abort the download
    And no unverified payload bytes may be passed to the parser
