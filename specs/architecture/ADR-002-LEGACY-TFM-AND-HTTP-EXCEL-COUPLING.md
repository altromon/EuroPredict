---
id: ADR-002-LEGACY-TFM-AND-HTTP-EXCEL-COUPLING
type: architecture-decision-record
title: Legacy .NET Standard 1.0/2.0 & .NET Core 3.1 Target Frameworks with WebClient Ingestion
status: accepted
version: "1.0.0"
schema-version: "1.0"
deciders:
  - "EuroPredict Original Author"
  - "System Architect Agent (Retro-Inception)"
decision-date: "2026-11-19"
affects-components:
  - CMP-MODEL-CONTRACTS-001
  - CMP-CALC-ENGINE-001
  - CMP-DATA-REPOSITORIES-001
  - CMP-CLI-CONSOLE-001
supersedes: null
superseded-by: null
---

# ADR-002: Legacy .NET Standard 1.0/2.0 & .NET Core 3.1 Target Frameworks with WebClient Ingestion

## 1. Context and Problem Statement
The `EuroPredict` brownfield codebase was originally authored targeting .NET Standard 1.0/2.0 libraries and a .NET Core 3.1 console host, using synchronous `System.Net.WebClient` and `ExcelDataReader` (`3.6.0`) to load both local file paths and HTTP/HTTPS draw datasets (`FR-INGEST-CSV-001`, `FR-INGEST-EXCEL-001`, `QR-RELIABILITY-001`).

## 2. Considered Technology Options
1. **Option A — Document As-Is Legacy Baseline (`netstandard1.0`/`netstandard2.0`/`netcoreapp3.1` + `WebClient`)**: Preserve and record the exact current target frameworks and `WebClient.DownloadData` + `ExcelDataReader` (`3.6.0`) + `System.Text.CodePagesEncodingProvider` ingestion mechanism during retro-inception.
2. **Option B — Immediate Unrequested Upgrade to .NET 8 + `HttpClient`**: Modernize target frameworks and HTTP clients prior to establishing the AI-SDLC baseline.

## 3. Decision Outcome
Chosen **Option A** for the retro-inception baseline:
- Core contracts, shared utilities, and `CombinationCalculator` target `netstandard1.0`.
- `CSVCombinationRepository` and `ExcelCombinationRepository` target `netstandard2.0` (using `WebClient.DownloadData` into `MemoryStream`, plus `ExcelDataReader` `3.6.0` and `System.Text.Encoding.CodePages` `4.5.1` for legacy `.xls` codepages).
- `EuroPredict.UI.SimpleConsole` targets `netcoreapp3.1` with `Microsoft.Extensions.Configuration.Json` `2.2.0`.

## 4. Consequences
- **Positive**: Accurate, non-breaking baseline for brownfield onboarding; `WebClient.DownloadData` handles both local file paths and HTTP/HTTPS URLs uniformly.
- **Negative / Trade-offs**: `netcoreapp3.1` is end-of-life, `netstandard1.0` limits modern API usage, and `WebClient` is obsolete in modern .NET—any future modernization should be scheduled as an explicit `CHG-*` increment superseding this ADR.

---

## 5. Revision History and Version Control

| Version | Date | Deciders | Decision Status | Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | System Architect Agent | Retro-inception baseline of As-Is constraints | RETRO-INCEPTION-001 |
