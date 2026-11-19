// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/fr-ingest-csv-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "a CSVCombinationRepository configured with a valid CombinationFactory"],
  ['given', "a CSV data source containing header \"FECHA,COMB. GANADORA,,,,,,ESTRELLAS,\" and row \"22/10/2021,12,17,22,33,39,,02,12\""],
  ['when', "GetCombinations is invoked"],
  ['then', "the repository returns 1 combination"],
  ['given', "the combination has Columns [12, 17, 22, 33, 39] and Stars [2, 12]"],
  ['given', "a CSV line \"<csv_line>\""],
  ['when', "CombinationFactory.CreateCombination parses the line"],
  ['then', "the parsed result is \"<expected_outcome>\""],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
