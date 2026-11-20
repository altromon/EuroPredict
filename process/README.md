# AI-SDLC: Process & Governance Framework

This directory hosts the deterministic governance, verification gates, and lifecycle protocols for AI-assisted software development.

## Core Protocols & Lifecycle
- **Phase 0 (Product Definition)**: `specs/product/` (Requirements `FR-*`, Use Cases `UC-*`, Actors `ACT-*`, Rules `BR-*`).
- **Phase 1 (System Architecture)**: `templates/architecture/` (arc42 + NAF v4, `CMP-*`, Decisions `ADR-*`).
- **Phase 2 (Cybersecurity Triad)**: `templates/security/` (Threat Actors `ACT-THREAT-*`, Abuse Cases `ABUSE-*`, Security Reqs `SEC-REQ-*`).
- **Phase 2b (Privacy-by-Design as Code)**: `templates/privacy/` (Data Assets `DATA-ASSET-*`, ROPA `PRIV-ROPA-*`, Privacy Reqs `PRIV-REQ-*`).
- **Phase 3 (Spec-Driven Delivery - SDD)**: `specs/changes/active/<change-id>/` (`proposal.md`, `spec.md`, `design.md`, `tasks.md`, `handoff.yaml`).
- **Phase 4 (Continuous Quality Gates & Verification)**:
  - Quality Gate: `npx aisdlc verify quality` (CC $\le 10$, MI $\ge 50$, LOC $\le 40$)
  - BDD Testing: `npx aisdlc verify testing`
  - Traceability: `npx aisdlc verify traceability`
  - Licenses: `npx aisdlc verify licenses`
  - Privacy & Data Protection: `npx aisdlc verify privacy`
  - Schemas: `npx aisdlc verify schemas`
  - Release Gates: `npx aisdlc check` / `npx aisdlc verify all`
