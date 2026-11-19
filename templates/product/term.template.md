---
id: TERM-NAME-001
type: term
title: Canonical Term Name
status: draft
version: "1.0.0"
schema-version: "1.0"
definition: "Canonical, formal, and unambiguous definition of the concept within the domain's ubiquitous language."
bounded-context: "FlightControl"
synonyms:
  - "Alternative Term"
  - "Technical Alias"
anti-terms:
  - "Confusing or Discouraged Term"
  - "Inaccurate Colloquial Concept"
related-terms:
  - TERM-RELATED-001
supersedes: null
superseded-by: null
---

# TERM-NAME-001: Canonical Term Name

## 1. Formal Definition (Ubiquitous Language)
> **Definition**: Canonical, formal, and unambiguous definition of the concept within the domain's ubiquitous language.

This concept establishes a shared semantic truth across domain experts, developers, and security engineers, eliminating ambiguous interpretations in specifications and code.

---

## 2. Bounded Context and Semantic Boundaries
- **Bounded Context**: `FlightControl`
- **Application Scope**: Valid across domain models, database entities, public APIs, and telemetry events within this context.

---

## 3. Terminology Guide (Correct vs. Incorrect Usage)

| Category | Term | Application Guideline |
| :--- | :--- | :--- |
| **Canonical** | `Canonical Term Name` | Mandatory usage in DTOs, interfaces, and formal documentation. |
| **Permitted Synonym** | `Alternative Term` | Permitted exclusively in user manuals or informal communication. |
| **Anti-Term (Forbidden)** | `Confusing or Discouraged Term` | **Forbidden**: Leads to domain confusion or semantic collision. |

---

## 4. Related Terms

- `TERM-RELATED-001`: Complementary concept within the same bounded context.

---

## 5. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-23 | Domain Architect / PO | Initial addition to domain glossary | CHG-INIT-001 |
