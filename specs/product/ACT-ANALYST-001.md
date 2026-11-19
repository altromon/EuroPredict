---
id: ACT-ANALYST-001
type: actor
title: Console User / Analyst
status: draft
version: "1.0.0"
schema-version: "1.0"
role-type: human-user
description: Human user who invokes EuroPredict.UI.SimpleConsole via CLI to obtain most-frequent or seed-conditioned EuroMillions combinations.
supersedes: null
superseded-by: null
---

# ACT-ANALYST-001: Console User / Analyst

## 1. Profile and Responsibility
The Console User / Analyst runs `EuroPredict.UI.SimpleConsole` locally to analyze historical EuroMillions draw frequencies and generate a recommended 5-column, 2-star combination. They configure the target historical data source path or URL in `appsettings.json` and optionally supply seed Columns and Stars as command-line arguments.

## 2. System Touchpoints
- **CLI Entry Point (`EuroPredict.UI.SimpleConsole`)**: Executes the console binary with `0` arguments (global most-common combination), `1` argument (comma-separated seed Columns), or `2` arguments (comma-separated seed Columns and seed Stars).
- **Configuration (`appsettings.json`)**: Sets the `CombinationsDatasource` key pointing to a local file or remote HTTP/HTTPS resource.
- **Standard Output (`Console.WriteLine`)**: Receives sorted `Columns: ...` and `Stars: ...` lines, or the `No Combination matches the provided requirements` message when no matching historical draws exist.

---

## 3. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is actor profile from brownfield codebase inspection | RETRO-INCEPTION-001 |
