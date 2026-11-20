---
id: JRN-PREDICT-001
type: journey
title: End-to-End Console Prediction Workflow
status: draft
version: "1.0.0"
schema-version: "1.0"
persona: ACT-ANALYST-001
stages:
  - "Data Source Configuration"
  - "CLI Invocation"
  - "Historical Ingestion and Frequency Analysis"
  - "Console Output Review"
touchpoints:
  - "appsettings.json configuration file"
  - "EuroPredict.UI.SimpleConsole CLI"
  - "ICombinationRepository (CSV/Excel)"
  - "Standard Output (Console)"
related-use-cases:
  - UC-LOAD-COMBINATIONS-001
  - UC-PREDICT-COMMON-001
  - UC-PREDICT-FILTERED-001
pain-points:
  - "Switching between CSV and Excel repositories currently requires recompiling Program.cs"
  - "Invalid CLI argument tokens are silently swallowed into an empty list"
supersedes: null
superseded-by: null
---

# JRN-PREDICT-001: End-to-End Console Prediction Workflow

## 1. Actor / Primary Persona Profile
- **Protagonist Actor**: `ACT-ANALYST-001`
- **Main Goal**: Obtain a frequency-based EuroMillions combination (5 Columns, 2 Stars) from historical draw data via the console.

---

## 2. Journey Stages Map

| Stage | User Goal | Touchpoints | Frictions / Pain Points |
| :--- | :--- | :--- | :--- |
| **1. Data Source Configuration** | Point EuroPredict to a local or remote CSV/Excel dataset | `appsettings.json` configuration file | Switching between CSV and Excel repositories requires code change in `Program.cs` |
| **2. CLI Invocation** | Request either global top numbers or seed-filtered numbers | `EuroPredict.UI.SimpleConsole` CLI | Invalid CLI argument tokens are silently ignored by `ConvertToIntCollection` |
| **3. Historical Ingestion and Frequency Analysis** | Load all historical draws and rank numbers by occurrence | `ICombinationRepository` (CSV/Excel) | Network/file errors propagate unhandled via `WebClient.DownloadData` |
| **4. Console Output Review** | Read the sorted recommended Columns and Stars | Standard Output (Console) | No frequency counts or summary table are printed in console mode |

---

## 3. Traceability to Use Cases and Product

- `UC-LOAD-COMBINATIONS-001`: Loads historical combinations from the configured `CombinationsDatasource`.
- `UC-PREDICT-COMMON-001`: Computes the globally most frequent 5 Columns and 2 Stars when no CLI arguments are provided.
- `UC-PREDICT-FILTERED-001`: Filters historical combinations by seed Columns/Stars and computes the most frequent 5 Columns and 2 Stars from the matching subset.

---

## 4. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is user journey from brownfield codebase inspection | RETRO-INCEPTION-001 |
