---
id: BC-NAME-001
type: bounded-context
title: "Bounded Context Name"
status: draft
version: "1.0.0"
schema-version: "1.0"
domain-type: core
strategic-classification:
  evolutionary-stage: custom-built
  business-model-role: differentiator
responsibilities:
  - "Primary domain capability or boundary responsibility"
ubiquitous-language-terms:
  - TERM-NAME-001
context-mapping:
  upstream-dependencies: []
  downstream-dependents: []
  integration-patterns:
    - pattern: Open Host Service / Published Language
      related-context: BC-ANOTHER-001
components-involved:
  - CMP-NAME-001
supersedes: null
superseded-by: null
---

# BC-NAME-001: Bounded Context Name

## 1. Context Purpose and Scope
Executive summary and clear linguistic boundary defining what is inside vs. outside this Bounded Context according to Domain-Driven Design (DDD).

### 1.1 Strategic Domain Classification
- **Domain Type**: `core` (Core Domain / Supporting Subdomain / Generic Subdomain)
- **Evolutionary Stage**: `custom-built` (Genesis / Custom-Built / Product / Commodity - Wardley Mapping)
- **Business Role**: Key differentiator creating competitive advantage for the product.

---

## 2. Ubiquitous Language & Domain Invariants
- **Core Entities & Aggregates**: High-level domain concepts encapsulated within this boundary.
- **Glossary References**:
  - `TERM-NAME-001`: Primary ubiquitous term definition.
- **Domain Invariants**: Critical business rules (`BR-*`) enforced exclusively within this boundary.

---

## 3. Context Mapping & Integration Relationships
Relationships with adjacent Bounded Contexts:
- **Upstream Contexts**: Contexts providing domain events, APIs, or contracts.
- **Downstream Contexts**: Contexts consuming models from this boundary.
- **Anti-Corruption Layers (ACL)**: Translations safeguarding ubiquitous language from external contamination.

---

## 4. Architecture Mapping
- **Physical Components (`CMP-*`)**: Components implementing aggregates, services, or adapters within this context.
- **Security Enclaves (`SEC-ENC-*`)**: Execution boundary or isolation tier.

---

## 5. Revision History

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | YYYY-MM-DD | Product Analyst / Architect | Initial Bounded Context definition | CHG-INIT-001 |
