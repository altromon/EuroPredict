using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Europredict.Core.ExcelCombinationRepositoryTests
{
    public class ExternalExcelSourceData
    {
        public static IEnumerable<object[]> TestData()
        {
            var testCases = new List<object[]>();

            string[] lines = File.ReadAllLines("ExistingExcelSourceData.txt");
            foreach (var line in lines)
            {
                var values = line.Split(",");
                object[] testCase = new object[] { values[0] };
                testCases.Add(testCase);
            }

            return testCases;
        }

    }
}
