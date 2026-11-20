---
id: QR-RELIABILITY-001
type: requirement
title: Resilient Malformed-Row and Input Handling
status: draft
version: "1.0.0"
schema-version: "1.0"
category: quality
derives-from:
  - UC-LOAD-COMBINATIONS-001
  - UC-PREDICT-FILTERED-001
verifiable-by: automated-unit-test
acceptance-format: declarative-prose
supersedes: null
superseded-by: null
---

# QR-RELIABILITY-001: Resilient Malformed-Row and Input Handling

## 1. Normative Statement
Data ingestion components MUST gracefully skip non-data rows (such as CSV headers, blank Excel rows, and `DBNull`/non-numeric cells) without aborting ingestion of valid historical draws.

> **As-Is Legacy Behavior & Limitation (`Program.ConvertToIntCollection`)**: In the current baseline, `Program.ConvertToIntCollection` silently catches parsing exceptions on invalid comma-separated CLI seed tokens and returns an empty `List<int>` (which `IsContained` treats as unconstrained). This silent empty-list fallback is an **As-Is legacy behavior/limitation** and is targeted for future hardening under `SEC-REQ-INPUT-001` (explicit input validation and rejection of malformed CLI arguments), resolving any requirement conflict with `SEC-REQ-INPUT-001`.

---

## 2. Upstream Traceability

| Dimension | Link / Artifact | Relationship Type | Status |
| :--- | :--- | :--- | :--- |
| **Product (Upstream)** | `UC-LOAD-COMBINATIONS-001` | Derived from Use Case | Validated |
| **Product (Upstream)** | `UC-PREDICT-FILTERED-001` | Derived from Use Case | Validated |

---

## 3. Acceptance Criteria (Declarative)
- `CSVCombinationRepository.CombinationFactory.CreateCombination` returns `null` when a row cannot be parsed into 5 Column integers and 2 Star integers, and `CombinationRepository.ExtractCombinations` omits `null` entries.
- `ExcelCombinationRepository.CombinationBuilder.ReadColumns` ignores `DBNull` and non-numeric strings (matching `[0-9]+` only), and `ExtractSheetCombinations` discards rows with 0 Columns or 0 Stars.
- `Program.ConvertToIntCollection` currently exhibits the As-Is legacy fallback of catching parsing exceptions on malformed CLI seed strings and returning an empty `List<int>` (targeted for replacement with strict validation under `SEC-REQ-INPUT-001`).

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Product Analyst Agent | Initial As-Is quality requirement from brownfield codebase inspection | RETRO-INCEPTION-001 |
