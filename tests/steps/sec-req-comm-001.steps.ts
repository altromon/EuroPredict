// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/security/sec-req-comm-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "a combination repository configured with DataSource \"<datasource_uri>\""],
  ['when', "GetCombinations is invoked"],
  ['then', "the repository must reject the request before initiating a network download"],
  ['given', "an ArgumentException or SecurityException must be raised indicating insecure transport"],
  ['given', "a combination repository configured with an HTTPS DataSource presenting an invalid or self-signed certificate"],
  ['then', "the TLS handshake must fail and abort the download"],
  ['given', "no unverified payload bytes may be passed to the parser"],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
