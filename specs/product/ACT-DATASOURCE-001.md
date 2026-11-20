---
id: ACT-DATASOURCE-001
type: actor
title: External Lottery Data Source
status: draft
version: "1.0.0"
schema-version: "1.0"
role-type: external-system
description: External file or HTTP/HTTPS endpoint providing historical EuroMillions draw combinations in CSV or Excel format.
supersedes: null
superseded-by: null
---

# ACT-DATASOURCE-001: External Lottery Data Source

## 1. Profile and Responsibility
The External Lottery Data Source supplies the historical EuroMillions draw dataset consumed by `ICombinationRepository` implementations (`EuroPredict.Core.CSVCombinationRepository` and `EuroPredict.Core.ExcelCombinationRepository`). Each draw record contains a draw date, 5 main numbers (Columns), and 2 bonus numbers (Stars).

## 2. System Touchpoints
- **CSV Feed (`EuroPredict.Core.CSVCombinationRepository`)**: Accessed via `WebClient.DownloadData(DataSource)` (supporting both local file paths and HTTP/HTTPS URLs such as Google Sheets CSV exports), with comma-separated rows formatted as `FECHA,C1,C2,C3,C4,C5,,S1,S2`.
- **Excel Workbook (`EuroPredict.Core.ExcelCombinationRepository`)**: Accessed via `WebClient.DownloadData(DataSource)` and parsed with `ExcelDataReader` across all worksheet `DataTable`s, locating `DateTime` cells followed by 5 Column integers and 2 Star integers.

---

## 3. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is external system actor profile from brownfield codebase inspection | RETRO-INCEPTION-001 |
