// AUTO-GENERATED RED BDD STEP DEFINITIONS (tests/features/fr-ingest-excel-001.feature)
import { BddStepRegistry } from '@aisdlc/core';

const STEP_SPECS: ReadonlyArray<readonly ['given' | 'when' | 'then', string]> = [
  ['given', "an ExcelCombinationRepository with CodePagesEncodingProvider registered"],
  ['given', "an Excel worksheet containing a title row and a draw row starting with DateTime \"2021-10-22\" followed by 12, 17, 22, 33, 39, 2, 12"],
  ['when', "GetCombinations is invoked"],
  ['then', "the repository returns 1 combination"],
  ['given', "the combination has Columns [12, 17, 22, 33, 39] and Stars [2, 12]"],
  ['given', "a worksheet row with first cell \"<first_cell_type>\" and numeric cells \"<cell_values>\""],
  ['when', "ExtractSheetCombinations processes the row"],
  ['then', "the row is \"<inclusion_result>\" in the returned combinations"],
];

export function registerSteps(registry: BddStepRegistry): void {
  for (const [method, text] of STEP_SPECS) {
    registry[method](text, () => {
      throw new Error(`RED: Step not implemented: ${text}`);
    });
  }
}
