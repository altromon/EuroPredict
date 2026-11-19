---
id: ABUSE-TAMPER-INPUT-001
type: abuse-case
title: Domain Integrity Corruption via Out-of-Range Draw Numbers or Malformed CLI Seeds
status: draft
version: "1.0.0"
schema-version: "1.0"
primary-threat-actor: ACT-THREAT-MALICIOUS-INPUT-001
targets-use-case: UC-PREDICT-FILTERED-001
stride-category: tampering
mitigated-by:
  - SEC-REQ-INPUT-001
asvs-controls:
  - "V5.1.1"
  - "V5.1.3"
supersedes: null
superseded-by: null
---

# ABUSE-TAMPER-INPUT-001: Domain Integrity Corruption via Out-of-Range Draw Numbers or Malformed CLI Seeds

## 1. Attack Vector and Exploitation Mechanics
1. `ACT-THREAT-MALICIOUS-INPUT-001` injects out-of-range integers into a CSV/Excel data source (e.g., Columns outside `1..50`, Stars outside `1..12`, or duplicate numbers within a draw) or passes malformed/excessive tokens in CLI arguments (`args[0]`, `args[1]`).
2. `CombinationFactory` and `CombinationBuilder` parse integers without enforcing EuroMillions domain range constraints, allowing invalid numbers into `CombinationsSummaryAdapter`.
3. `Program.ConvertToIntCollection` catches `FormatException` and silently returns an empty `List<int>`, causing `CombinationsCalculator.IsContained` to treat the malformed filter as unconstrained (`true`) and silently return unfiltered results instead of rejecting the invalid input.

## 2. Attack Preconditions
- Untrusted draw rows in CSV/Excel or unvalidated CLI arguments passed to `EuroPredict.UI.SimpleConsole`.

## 3. Potential Impact
- **Integrity**: Invalid lottery numbers appear in frequency rankings, or malformed CLI seed filters silently fall back to unconstrained predictions without warning the analyst.

---

## 4. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-11-19 | Threat Modeler Agent | Initial As-Is abuse case for domain/CLI input tampering | RETRO-INCEPTION-001 |
