---
id: MAN-USER-001
type: user-manual
title: "User Manual: Application Name"
status: draft
version: "1.0.0"
schema-version: "1.0"
applies-to-version: "v1.0.0"
target-audience:
  - ACT-ROLE-USER-001
  - ACT-OPERATOR-002
allowed-roles:
  - ACT-ROLE-USER-001
  - ACT-OPERATOR-002
journeys-covered:
  - JRN-JOURNEY-001
use-cases-covered:
  - UC-ACTION-001
supersedes: null
superseded-by: null
---

# MAN-USER-001: User Manual - Application Name

## 1. System Purpose and Audience

Concisely describes the software's purpose, the operational value it delivers to end users, and the general context in which it operates.

- **Application / System**: `[System Name or Bounded Context]`
- **Software Version**: `v1.0.0`
- **Target Audience**: `[Functional roles and user profiles addressed]`

---

## 2. User Role Catalog and Permissions Matrix (RBAC)

This section formally defines the roles authorized to access and operate the application, as well as the privileges assigned to each.

### 2.1 Authorized Role Catalog

| Role Identifier | Role Name | Associated Actor | Description & Responsibility | Enclave / Access Level |
| :--- | :--- | :--- | :--- | :--- |
| **`ROLE_USER`** | Standard User | `ACT-ROLE-USER-001` | Data queries and nominal business flow execution. | Corporate Network / Web UI |
| **`ROLE_OPERATOR`** | System Operator | `ACT-OPERATOR-002` | Streaming supervision, alert management, and command control. | DMZ Enclave / Operational Console |

### 2.2 Access and Capabilities Matrix by Role

| Application Capability / Module | `ROLE_USER` | `ROLE_OPERATOR` | Authentication Mode |
| :--- | :---: | :---: | :--- |
| Status Query and Dashboard Visualizations | ✅ Allowed | ✅ Allowed | Bearer Token (OIDC/JWT) |
| Business Journey Execution (`JRN-JOURNEY-001`) | ✅ Allowed | ✅ Allowed | Bearer Token (OIDC/JWT) |
| Real-Time Streaming and Operational Control | ❌ Denied | ✅ Allowed | mTLS + Device X.509 Certificate |
| Global Parameters Configuration & Connectivity | ❌ Denied | ✅ Allowed | Administrator Credential |

---

## 3. Version and User Platform Compatibility Matrix

This section specifies cross-compatibility between the current application version and user environments, clients, and formats.

### 3.1 Client-Server Compatibility (APIs and SDKs)

| Application Version (Backend) | Compatible CLI Version | Compatible SDK Version | Compatibility Status | Migration Notes |
| :---: | :---: | :---: | :---: | :--- |
| **`v1.0.0`** (Current) | `>= v1.0.0` | `>= v1.0.0` | ✅ Fully Supported | Active canonical version. |
| **`v1.0.0`** (Current) | `v0.9.x` | `v0.9.x` | ⚠️ Degraded Mode | Requires upgrade before next minor. |
| **`v1.0.0`** (Current) | `< v0.9.0` | `< v0.9.0` | ❌ Incompatible / Deprecated | Legacy endpoints decommissioned. |

### 3.2 Homologated Browsers and Client Platforms

| Platform / Environment | Minimum Supported Versions | Support Level | Known Constraints |
| :--- | :--- | :---: | :--- |
| **Google Chrome / Chromium** | Version `>= 120` | Tier 1 (Official) | Optimal WebSockets and WebGL performance. |
| **Mozilla Firefox** | Version `>= 115 ESR` | Tier 1 (Official) | Full client certificate support. |
| **Safari / WebKit** | Version `>= 17.0` | Tier 2 (Functional) | Requires explicit mTLS enablement in macOS Keychain. |
| **Terminal / CLI (Linux / macOS / Windows)** | Node.js `>= 18` or Bash `>= 5.0` | Tier 1 (Official) | Full support on PowerShell 7+ and bash. |

### 3.3 Configuration Files and Data Formats Compatibility

| Format / Artifact | Source Version Supported | Automatic Conversion | User Action Required |
| :--- | :---: | :---: | :--- |
| **`config.yaml`** | `v0.9.x` | ✅ Yes | None. Application migrates deprecated keys on startup. |
| **Export Profiles (`.json`)** | `v0.9.x` | ✅ Yes | 100% backward compatible. |

---

## 4. Application Installation, Access, and Configuration

### 4.1 Access Channels and Prerequisites

- **Web Portal / UI**: Access URL (e.g., `https://app.system.internal`).
- **Command Line (CLI)**: Executable binary or package (e.g., `npx system-cli` or `./system-client`).
- **Supported Browsers or Platforms**: Chrome >= 120, Firefox >= 120, Linux/macOS/Windows terminal.

### 4.2 Client Configuration and Environment Variables

The application allows parameterizing its behavior via environment variables or local configuration files:

| Parameter / Variable | Type | Default Value | Required | Description |
| :--- | :--- | :--- | :---: | :--- |
| `API_ENDPOINT` | URL | `https://api.system.internal/v1` | Yes | Base backend API endpoint. |
| `WS_STREAM_URL` | WSS URL | `wss://stream.system.internal/telemetry` | No | Real-time telemetry or notifications socket. |
| `CLIENT_TIMEOUT_MS` | Integer | `5000` | No | Network response timeout in milliseconds. |
| `AUTH_PROFILE` | String | `production` | No | Active authentication profile (`development`, `staging`, `production`). |

### 4.3 Local Configuration File (`config.yaml` / `.env`)

Example recommended configuration for operator workstations:

```yaml
# Client configuration file: config.yaml
profile: production
network:
  api_endpoint: "https://api.system.internal/v1"
  stream_url: "wss://stream.system.internal/telemetry"
  timeout_ms: 5000
  retry_attempts: 3

security:
  tls_verify: true
  client_cert_path: "/etc/pki/client/operator.crt"
  client_key_path: "/etc/pki/client/operator.key"

ui:
  theme: "dark"
  refresh_interval_sec: 1
```

---

## 5. Step-by-Step Journey Execution Guide (`JRN-*`)

Each subsection documents an end-to-end Journey, specifying authorized execution roles, starting conditions, and the step-by-step operating procedure.

### 5.1 `JRN-JOURNEY-001`: User Journey Name

- **Linked Use Case**: `UC-ACTION-001`
- **Primary Actor**: `ACT-ROLE-USER-001`
- **Authorized Roles**: `ROLE_USER`, `ROLE_OPERATOR`
- **Required Execution Enclave**: Corporate Intranet or VPN access

#### A. Preconditions
1. User must have an active and authenticated session with their authorized role.
2. The base entity or resource must be in `ACTIVE` state.

#### B. Step-by-Step Procedure

1. **Step 1: Operation Initiation**
   - **Action**: User navigates to the execution section or invokes the corresponding command:
     ```bash
     system-cli execute --journey JRN-JOURNEY-001 --input payload.json
     ```
   - **Required Input**: Resource identifier and validated parameters.
   - **Expected On-Screen Response**: System displays confirmation dialog with operation summary.

2. **Step 2: Validation and Submission**
   - **Action**: User reviews parameters and confirms execution.
   - **System Behavior**: Client locally validates data schema and transmits signed request.

3. **Step 3: Observable Outcome Verification**
   - **Success Outcome**: Console / interface displays success message `MSG-SUCCESS-001` with generated unique transaction ID.
   - **Final State**: Resource transitions to `PROCESSED` state.

#### C. Alternative Flows and Exception Handling
- **Unauthorized Access**: If an unauthorized role attempts to invoke the journey, the system blocks the action, outputs code `MSG-ERR-AUTH-403`, and records the access attempt in the audit trail.
- **Connection Loss in Transit**: If connection is lost during step 2, client retries with exponential backoff up to 3 times before notifying user.

---

## 6. System Messages Catalog and Response Codes

Structured matrix of key informational, warning, and error messages emitted by the application during its operational lifecycle:

| Code | Severity | Literal Screen Message | Trigger / Context | Meaning for User | Recommended Corrective Action |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **`MSG-SYS-100`** | `INFO` | `"Connection established successfully with server"` | Successful network handshake upon initializing session. | Application is online and ready to process commands. | None. Normal operation. |
| **`MSG-SYS-200`** | `INFO` | `"Operation completed successfully. Transaction ID: [TX-ID]"` | Successful completion of a Journey or Use Case. | Transaction was persisted and acknowledged by backend. | Save `TX-ID` for tracking if required. |
| **`MSG-WARN-301`** | `WARN` | `"High link latency (>500ms). Degraded mode active"` | Response metric exceeds real-time threshold. | Screen data may experience slight refresh delays. | Check network connection quality or bandwidth. |
| **`MSG-WARN-302`** | `WARN` | `"Session expiring in 5 minutes"` | Authentication token approaching TTL expiry. | User will be logged out unless credential is refreshed. | Renew session by clicking alert or re-authenticating. |
| **`MSG-ERR-401`** | `ERR-USER` | `"Invalid credentials or expired token. Access denied"` | Login authentication failure or expired token. | User identity could not be validated. | Re-enter credentials or request a new token. |
| **`MSG-ERR-403`** | `ERR-USER` | `"Unauthorized action for assigned role [ROLE]"` | Attempted execution of Journey or command outside RBAC. | User role lacks permissions for this resource. | Contact administrator to request permission expansion. |
| **`MSG-ERR-422`** | `ERR-USER` | `"Invalid payload: field [FIELD] does not match schema"` | Syntactic validation or business rule (`BR-*`) failure. | User-supplied data is invalid. | Correct indicated fields according to business rule. |
| **`MSG-ERR-503`** | `ERR-SYS` | `"Service temporarily unavailable. Retrying..."` | Backend gateway unreachable or under scheduled maintenance. | Central server is not responding to requests. | Await automatic retries or check system status page. |

---

## 7. Frequently Asked Questions (FAQ) and Support

- **What should I do if I lose connectivity while executing a Journey?**  
  The application implements idempotency mechanisms. Once network connectivity is restored, verify resource status before retrying to prevent duplicate transactions.
- **How do I request a user role change?**  
  Contact the Security Administrator (`SecOps`) referencing your user ID and operational justification for the role change.

---

## 8. Revision History and Version Control

| Version | Date | Author / Agent | Change Description | Change Reference (Change/PR) |
| :--- | :--- | :--- | :--- | :--- |
| **1.0.0** | 2026-09-15 | Documentation & Product Team | Initial User Manual generation | CHG-009 |
