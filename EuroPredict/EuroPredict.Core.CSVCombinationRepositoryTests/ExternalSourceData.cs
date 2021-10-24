using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroPredict.Core.CSVCombinationRepositoryTests
{
    public class ExternalSourceData
    {
        public static IEnumerable<object[]> TestData()
        {
            var testCases = new List<object[]>();

            string[] lines = File.ReadAllLines("ExistingSourceData.txt");
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
