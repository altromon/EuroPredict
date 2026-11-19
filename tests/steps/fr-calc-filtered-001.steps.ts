// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/fr-calc-filtered-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "a historical dataset of EuroMillions combinations loaded into memory"],
  ['given', "historical draws containing Columns [12, 17, 22, 33, 39] with Stars [2, 12]"],
  ['when', "EuroPredict.UI.SimpleConsole is invoked with args \"12,22\" and \"2\""],
  ['then', "standard output contains sorted \"Columns: \" including 12 and 22"],
  ['given', "standard output contains sorted \"Stars: \" including 2"],
  ['given', "seed Columns \"<seed_columns>\" and seed Stars \"<seed_stars>\""],
  ['when', "GetMostProbableCombinationFromNumber filters the historical dataset"],
  ['then', "the console outputs \"<expected_output_prefix>\""],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
