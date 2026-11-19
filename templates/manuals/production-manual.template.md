---
id: MAN-PROD-001
type: production-manual
title: "Production & Operations Manual: Application Name"
status: draft
version: "1.0.0"
schema-version: "1.0"
applies-to-version: "v1.0.0"
target-audience:
  - devops-engineer
  - sre-engineer
  - release-manager
  - secops
components-covered:
  - CMP-NAME-001
enclaves-involved:
  - SEC-ENC-DMZ-001
supersedes: null
superseded-by: null
---

# MAN-PROD-001: Production & Operations Manual - Application Name

## 1. Deterministic Release Regeneration (Reproducible Builds)

This section contains the specifications and directives required to deterministically reproduce identical bit-by-bit application artifacts or releases from their Git tag.

### 1.1 Source Code Baseline

- **Canonical Repository**: `https://github.com/organization/repo-name.git`
- **Immutable Git Release Tag**: `v1.0.0`
- **Commit SHA-256 / SHA-1**: `[VERIFIED_HEXADECIMAL_COMMIT_SHA]`
- **Clean Checkout Command**:
  ```bash
  git clone --recurse-submodules https://github.com/organization/repo-name.git
  cd repo-name
  git checkout tags/v1.0.0
  git submodule update --init --recursive
  ```

### 1.2 Frozen Toolchains and Build Tool Matrix

To prevent discrepancies in binary or package generation, strict usage of pinned versions of the following utilities is enforced:

| Tool / Runtime | Exact Version | Toolchain Checksum / Digest | Purpose |
| :--- | :--- | :--- | :--- |
| **Node.js LTS** | `v22.12.0` | `sha256:node-v22.12.0-linux-x64.tar.xz` | Execution and transpilation runtime |
| **pnpm** | `9.15.0` | `sha256:pnpm-v9.15.0` | Deterministic monorepo package manager |
| **TypeScript (tsc)** | `5.7.2` | Pinned in `devDependencies` | Strict ECMAScript compiler |
| **Docker BuildKit** | `v0.18.0` | `moby/buildkit:v0.18.0` | Hermetic OCI packaging engine |

### 1.3 Libraries, Dependencies, and Vertex Graph

- **Immutable Lockfile**: `pnpm-lock.yaml` (or `Cargo.lock`, `go.sum`, `requirements.lock`). Installations with open resolution are forbidden (`pnpm install --frozen-lockfile` mandatory).
- **SBOM (Software Bill of Materials) Inventory**:
  - Format: CycloneDX JSON v1.5 / SPDX v2.3.
  - Compiled SBOM path: `reports/sbom-cyclonedx.json`.
- **OSS License Governance**:
  - Binding policy: `license-policy.yaml`.
  - Library vertex pre-verification:
    ```bash
    pnpm verify:licenses
    ```

### 1.4 Step-by-Step Deterministic Build Procedure

1. **Step 1: Hermetic Dependency Installation**
   ```bash
   pnpm install --frozen-lockfile --ignore-scripts
   ```

2. **Step 2: Integrity and Strict Type Checking**
   ```bash
   pnpm typecheck
   ```

3. **Step 3: Artifact Compilation**
   ```bash
   export SOURCE_DATE_EPOCH=$(git log -1 --pretty=%ct)
   pnpm build
   ```

4. **Step 4: Cryptographic Verification of Generated Artifact**
   ```bash
   sha256sum dist/app.bundle.js
   # Compare against the canonical checksum registered in official release:
   # Expected: [CANONICAL_SHA256_CHECKSUM]
   ```



5. **Step 5: Sigstore / Cosign Signature and SLSA Provenance Verification**
   ```bash
   # Verify npm package provenance from registry
   npm audit signatures

   # Verify package tarball with Sigstore / Cosign (Keyless via OIDC identity)
   cosign verify-blob \
     --bundle release-artifacts/package.tgz.bundle \
     --certificate-identity-regexp "^https://github.com/organization/repo-name/.*$" \
     --certificate-oidc-issuer "https://token.actions.githubusercontent.com" \
     release-artifacts/package.tgz

   # Verify SLSA Build Provenance Attestation using GitHub CLI
   gh attestation verify release-artifacts/package.tgz --repo organization/repo-name
   ```

---

## 2. Version, Infrastructure, and Migration Compatibility Matrix

This section defines technical interoperability boundaries between the release version and the deployment ecosystem, ensuring safe Zero-Downtime transitions.

### 2.1 Platform and Execution Runtime Compatibility

| Infrastructure Component | Approved Version Range | Recommended Version | Support Status |
| :--- | :--- | :--- | :---: |
| **Kubernetes Cluster (K8s)** | `>= 1.28` and `<= 1.31` | `1.30.2` | ✅ Certified |
| **Container Runtime (CRI)** | containerd `>= 1.7` / CRI-O `>= 1.28` | containerd `1.7.15` | ✅ Certified |
| **Base Operating System (Host)** | Ubuntu 22.04 LTS / RHEL 9.2+ | Ubuntu 22.04 LTS (Kernel 6.x) | ✅ Homologated |
| **Language Runtime (Production)** | Node.js `>= 20.x` and `<= 22.x` | `v22.12.0 LTS` | ✅ Hermetic |

### 2.2 Data Schema and Migration Compatibility (N-1 Support)

To enable Canary or Blue-Green continuous deployments without transaction loss:

| Release Version | DB Schema Version | Compatible with $N-1$ Code | Schema Migration Status |
| :---: | :---: | :---: | :--- |
| **`v1.0.0`** (Current) | `SCHEMA-v1.0` | ✅ Yes (supports `v0.9.x` code) | Non-destructive additive migration (no column renaming). |
| **`v0.9.x`** | `SCHEMA-v0.9` | ✅ Previous baseline | Fully compatible during Canary drain period. |

### 2.3 Interoperability Between Architecture Components (`CMP-*`)

| Dependent Component | Consumed Component | Minimum Compatible Versions | Binding Protocol / Contract |
| :--- | :--- | :---: | :--- |
| `CMP-NAME-001` (Gateway) | `CMP-NAME-002` (Backend) | `>= v1.0.0` | gRPC / Protobuf v3 (`contract_spec.proto`) |
| `CMP-NAME-001` (Gateway) | Messaging Bus / Kafka | `>= 3.5.0` | Kafka v2 Protocol with TLS mTLS |

### 2.4 Homologated Upgrade & Rollback Paths

| Source Version | Direct Jump to `v1.0.0` | Intermediate Migration Required | Direct Rollback Procedure |
| :---: | :---: | :---: | :---: |
| **`v0.9.1`** | ✅ Allowed | ❌ Not required | `kubectl rollout undo` without data loss. |
| **`v0.9.0`** | ✅ Allowed | ❌ Not required | `kubectl rollout undo` without data loss. |
| **`< v0.9.0`** | ❌ Blocked | ✅ Must upgrade to `v0.9.1` first | Requires restore from backup snapshot. |

---

## 3. CI/CD Architecture and Pipelines

### 3.1 Hierarchical Branching Model and Integration Triggers

Following the AI-SDLC 4-tier standard:

```text
TIER 1: main (Production)
  ▲
  └── PR Release Gate (Manual Tech Lead / Release Manager Approval)
        │
TIER 2: release/v1.0.0 (Open Version Branch)
  ▲
  └── PR Feature Gate (Lint + Tests + Quality Gate + SAST + SBOM)
        │
TIER 3: feat/CHG-001-* (Feature / Bug)
```

- **Pull Request Trigger**: Executes linters, full test battery, and governance verifications without deployment.
- **Release Tag Trigger (`v*.*.*`)**: Executes the full hermetic build pipeline, image signing, and continuous deployment.

### 3.2 CI/CD Pipeline Flowchart

```mermaid
flowchart LR
    A["Git Push / Tag v1.0.0"] --> B["Hermetic Checkout"]
    B --> C["Linters & Typecheck"]
    C --> D["Vitest: Unit & BDD"]
    D --> E["Quality Gate: CC & MI"]
    E --> F["SAST & Secret Scan"]
    F --> G["SBOM & License Gate"]
    G --> H["Deterministic Build"]
    H --> I["Cosign / SLSA Signing"]
    I --> J["Push to OCI Registry"]
    J --> K["Deploy to DMZ Enclave"]
```

### 3.3 Release Gate Contracts and Thresholds

| Quality Gate | Tool / Command | Required Threshold | Action on Non-Compliance |
| :--- | :--- | :--- | :--- |
| **Lint & Typecheck** | `pnpm typecheck` | Zero errors, zero `any` | **Immediate failure** |
| **Automated Tests** | `pnpm test:all` | 100% passed, Coverage >= 85% | **Immediate failure** |
| **Cyclomatic Complexity** | `aisdlc verify quality` | CC <= 10 per function | **Release blocked** |
| **Maintainability Index** | `aisdlc verify quality` | MI >= 50.0 / 100 | **Release blocked** |
| **360° Traceability** | `aisdlc verify traceability` | 100% of requirements linked | **Release blocked** |
| **OSS Licenses** | `aisdlc verify licenses` | Zero forbidden/viral licenses | **Release blocked** |
| **Cryptographic Signature & SLSA** | `cosign verify-blob` / `gh attestation verify` | Valid signature and SLSA provenance from GitHub Actions OIDC | **Deployment rejected** |

---

## 4. Production Deployment Strategy and Procedure

### 4.1 Infrastructure Prerequisites and Network Enclaves

- **Target Enclave**: `SEC-ENC-DMZ-001` (Strict segmentation without direct uncontrolled outbound internet access).
- **Enabled Network Ports**:
  - Port `8443/TCP`: Secure WebSocket ingestion with mutual TLS (mTLS) authentication.
  - Port `9090/TCP`: Prometheus metrics (internal management network only).
- **Secrets and Certificate Management**:
  - Server TLS and client root CA certificates mounted via immutable secret at `/etc/pki/tls/`.
  - Embedding cryptographic keys in environment variables or images is strictly forbidden.

### 4.2 Rollout Strategy

A **Canary strategy with progressive telemetry metrics analysis** is utilized:
1. 10% of traffic is routed to the new version for 15 minutes.
2. If the 5xx error rate is `<= 0.01%` and p99 latency is `<= 50ms`, traffic is promoted to 100%.

### 4.3 Step-by-Step Deployment Procedure

1. **Step 1: Pre-deployment and State Snapshot**
   ```bash
   # Check cluster health and record baseline
   kubectl get pods -n production -l app=system
   kubectl exec -it system-db-0 -- /backup/create-snapshot.sh
   ```

2. **Step 2: Deployment Manifest Application**
   ```bash
   kubectl apply -f deploy/production/canary-deployment.yaml
   kubectl rollout status deployment/system-service -n production --timeout=180s
   ```

3. **Step 3: Health Verification and Smoke Tests**
   ```bash
   curl -f --cacert /etc/pki/tls/ca.crt https://127.0.0.1:8443/healthz
   curl -f --cacert /etc/pki/tls/ca.crt https://127.0.0.1:8443/readyz
   pnpm test:example
   ```

### 4.4 Rollback Plan (Immediate Rollback)

- **Objective Rollback Activation Criteria**:
  - mTLS handshake failure rate `> 1.0%`.
  - Ingestion p99 latency `> 100ms` for 2 continuous minutes.
  - Critical `CrashLoopBackOff` alert on more than 20% of instances.
- **Rollback Execution Command**:
  ```bash
  kubectl rollout undo deployment/system-service -n production
  kubectl rollout status deployment/system-service -n production
  ```

---

## 5. Likely Errors Resolution and Troubleshooting (Runbooks)

This section documents the most frequent runtime and deployment failures along with diagnosis and immediate mitigation procedures.

### 5.1 Frequent Incidents and Solutions Matrix

#### Incident 1: Dependency Vertex Inconsistency or Drift (Build Failure)
- **Symptom**: `pnpm install --frozen-lockfile` command fails in CI with error `ERR_PNPM_LOCKFILE_OUTDATED`.
- **Root Cause**: `package.json` was modified without updating `pnpm-lock.yaml`.
- **Diagnosis**:
  ```bash
  git diff HEAD^ package.json pnpm-lock.yaml
  ```
- **Mitigation**:
  1. On the feature branch, run `pnpm install` locally to regenerate the deterministic lockfile.
  2. Verify no restricted-license dependencies were introduced via `pnpm verify:licenses`.
  3. Commit both files together in an atomic commit.

---

#### Incident 2: mTLS Handshake Failure or Enclave Certificate Rejected
- **Symptom**: Clients receive error `ERR_TLS_CERT_ALTNAME_INVALID` or connection closed with TLS alert `certificate_unknown (46)`.
- **Root Cause**: Internal root CA mounted on server does not match the client certificate issuer CA, or certificate expired.
- **Diagnosis**:
  ```bash
  openssl s_client -connect 127.0.0.1:8443 -CAfile /etc/pki/tls/ca.crt -cert /etc/pki/tls/client.crt -key /etc/pki/tls/client.key
  openssl x509 -in /etc/pki/tls/ca.crt -noout -dates -issuer -subject
  ```
- **Mitigation**:
  1. Verify expiration date of certificate mounted in cluster Secret.
  2. If expired, rotate the certificate by injecting the new pair from HSM / Vault storage.
  3. Orderly restart pods with `kubectl rollout restart deployment/system-service`.

---

#### Incident 3: Release Gate Rejection in CI/CD Due to Quality Metrics
- **Symptom**: Pipeline aborts with verdict `Quality Gate REJECTED` (Cyclomatic Complexity > 10 or Maintainability < 50).
- **Root Cause**: Introduction of complex nested logic (multiple `if/else`, extensive `switch`, or branching loops).
- **Diagnosis**:
  ```bash
  pnpm verify:quality
  ```
- **Mitigation**:
  1. Inspect the violation report in `reports/QUALITY_GATE_REPORT.md` to identify the offending function.
  2. Decompose function into cohesive private helper methods following the rule of maximum 40 lines per function.
  3. Re-run `pnpm verify:quality` until green verdict is confirmed. Adding suppression comments (`// @ts-ignore`) is forbidden.

---

#### Incident 4: CrashLoopBackOff Due to Memory Leak or Connection Limit
- **Symptom**: Pod is terminated with exit code `ExitCode: 137` (`OOMKilled`) after a load spike.
- **Root Cause**: Undrained socket buffer accumulation during high-frequency bursts.
- **Diagnosis**:
  ```bash
  kubectl describe pod system-service-xxx -n production | grep -i oom
  kubectl logs system-service-xxx -n production --previous
  ```
- **Mitigation**:
  1. Temporarily adjust memory limits in Kubernetes manifest (`resources.limits.memory`) if the load burst is legitimate.
  2. Apply backpressure or discard expired packets in accordance with business rules (`BR-*`).
  3. Run local benchmark with `pnpm test:all` to audit memory consumption.

---

### 5.2 Escalation Protocol and Critical Incident Management

| Severity Level | Impact Criterion | Maximum Response Time | Roles Involved |
| :--- | :--- | :---: | :--- |
| **SEV-1 (Critical)** | Complete service outage or security breach in enclave. | 15 minutes | On-call SRE + Lead Architect + SecOps |
| **SEV-2 (Major)** | Severe service degradation, massive retries without outage. | 1 hour | On-call SRE + Tech Lead |
| **SEV-3 (Minor)** | Isolated node incident without global SLA impact. | 4 hours | Support Engineer / DevOps |

---

## 6. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-15 | Operations & DevOps Team | Initial Production Manual generation | CHG-009 |
