// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/security/sec-req-dos-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "a DataSource returning a stream larger than the configured maximum byte limit"],
  ['when', "GetCombinations is invoked"],
  ['then', "ingestion must abort immediately upon exceeding the byte limit"],
  ['given', "an InvalidOperationException must be raised without exhausting process memory"],
  ['given', "an HTTPS DataSource that stalls without sending response bytes"],
  ['then', "the HTTP client must cancel the request after the configured timeout"],
  ['given', "no indefinite process hang may occur"],
  ['given', "an Excel DataSource containing truncated or malformed OOXML/BIFF bytes"],
  ['then', "the repository must fail cleanly with a controlled exception or empty safe result"],
  ['given', "no sensitive stack traces may be exposed to standard output"],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
