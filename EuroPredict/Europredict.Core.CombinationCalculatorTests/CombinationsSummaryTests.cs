using Europredict.Core.CombinationCalculator;
using EuroPredict.Model.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Europredict.Core.CombinationCalculatorTests
{
    [TestClass]
    public class CombinationsSummaryTests
    {
        CombinationsSummary combinationsSummary;

        [TestInitialize]
        public void Setup()
        {
            combinationsSummary = new CombinationsSummary()
            {
                ColumnsSummary = InititalizeSummaryData(50),
                StarsSummary = InititalizeSummaryData(9)
            };
        }

        public IEnumerable<INumberInfo> InititalizeSummaryData(int maxValue)
        {
            var summaryData = new List<INumberInfo>();
            for(int i = 1; i<=maxValue;i++)
            {
                var numberInfo = new NumberInfo() { Number = i, TotalTimes = i };
                summaryData.Add(numberInfo);
            }
            return summaryData;
        }

        [TestMethod]
        [DataRow(1,1)]
        [DataRow(30, 30)]
        [DataRow(50, 50)]
        [DataRow(51, 0)]
        public void GetCollectionValueTotalTimesTest_ValidInputs_ExpectedData(int value, int expected)
        {
            //Setup
            var collection = combinationsSummary.ColumnsSummary;

            //Act
            var actual = combinationsSummary.GetCollectionValueTotalTimes(collection, value);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
