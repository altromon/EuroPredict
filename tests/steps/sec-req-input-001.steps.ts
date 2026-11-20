// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/security/sec-req-input-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "a parsed draw row containing Column \"<col_value>\" and Star \"<star_value>\""],
  ['when', "domain validation evaluates the draw record"],
  ['then', "the record must be rejected as invalid"],
  ['given', "it must not be included in CombinationsSummaryAdapter frequency counts"],
  ['given', "EuroPredict.UI.SimpleConsole is invoked with seed argument \"<cli_arg>\""],
  ['when', "Program.Main parses the CLI seed tokens"],
  ['then', "the application must reject the invalid seed argument with a clear validation message"],
  ['given', "it must not silently execute an unconstrained prediction"],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
