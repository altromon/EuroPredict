// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/fr-calc-common-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "a historical dataset of EuroMillions combinations loaded into memory"],
  ['given', "historical combinations where Columns 23, 19, 44, 50, 5 and Stars 2, 8 have the highest TotalTimes"],
  ['when', "EuroPredict.UI.SimpleConsole is invoked with 0 arguments"],
  ['then', "standard output contains \"Columns: 5,19,23,44,50\""],
  ['given', "standard output contains \"Stars: 2,8\""],
  ['given', "a ColumnsSummary with <distinct_columns> distinct numbers and StarsSummary with <distinct_stars> distinct stars"],
  ['when', "GetCombinationWithMostCommonNumbers is called"],
  ['then', "the resulting combination has <expected_columns> Columns and <expected_stars> Stars"],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
