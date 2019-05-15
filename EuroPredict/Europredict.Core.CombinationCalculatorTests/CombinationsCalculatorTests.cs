using EuroPredict.Core.CombinationCalculator;
using EuroPredict.Model.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroPredict.Core.CombinationCalculatorTests
{
    [TestClass]
    public class CombinationsCalculatorTests
    {
        CombinationsCalculator sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new CombinationsCalculator();
        }

        [TestMethod]
        public void GetMostProbableNumberInfoTest_NoData_ExpectedNull()
        {
            //Act
            var actual = sut.SortMostProbableNumberInfos(Enumerable.Empty<INumberInfo>());

            //Assert
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void GetMostProbableNumberInfoTest_ValidData_Expected3()
        {
            //Setup
            var collection = new List<INumberInfo>()
            {
                new NumberInfo { Number = 1, TotalTimes = 1 },
                new NumberInfo { Number = 2, TotalTimes = 5 },
                new NumberInfo { Number = 3, TotalTimes = 100 },
                new NumberInfo { Number = 4, TotalTimes = 16 },
                new NumberInfo { Number = 5, TotalTimes = 81 },
            };

            //Act
            var actual = sut.SortMostProbableNumberInfos(collection);

            //Assert
            Assert.AreEqual(3, actual.FirstOrDefault().Number);
            Assert.AreEqual(1, actual.LastOrDefault().Number);
        }

        [TestMethod]
        public void GetCombinationWithMostCommonNumbers_NullData_ExpectedNullValue()
        {
            //Act
            var actual = sut.GetCombinationWithMostCommonNumbers(new CombinationsSummary());

            //Assert
            Assert.IsNull(actual.Columns);
            Assert.IsNull(actual.Stars);
        }

        [TestMethod]
        public void GetCombinationWithMostCommonNumbers_EmptyData_ExpectedEmptyValue()
        {
            //Act
            var actual = sut.GetCombinationWithMostCommonNumbers(new CombinationsSummary());

            //Assert
            Assert.IsNull(actual.Columns);
            Assert.IsNull(actual.Stars);
        }

        [TestMethod]
        public void GetCombinationWithMostCommonNumbers_ValidData_ExpectedValue()
        {
            //Setup
            var columnsSummary = new List<INumberInfo>()
            {
                new NumberInfo { Number = 1, TotalTimes = 1 },
                new NumberInfo { Number = 2, TotalTimes = 5 },
                new NumberInfo { Number = 3, TotalTimes = 100 },
                new NumberInfo { Number = 4, TotalTimes = 16 },
                new NumberInfo { Number = 5, TotalTimes = 81 },
                new NumberInfo { Number = 20, TotalTimes = 41 },
                new NumberInfo { Number = 35, TotalTimes = 100 },
                new NumberInfo { Number = 13, TotalTimes = 1 },
                new NumberInfo { Number = 8, TotalTimes = 8 }
            };

            var expectedColumns = new List<int>() { 3, 35, 5, 20, 4 };

            var starsSummary = new List<INumberInfo>()
            {
                new NumberInfo { Number = 1, TotalTimes = 11 },
                new NumberInfo { Number = 2, TotalTimes = 25 },
                new NumberInfo { Number = 3, TotalTimes = 10 },
                new NumberInfo { Number = 4, TotalTimes = 16 },
                new NumberInfo { Number = 5, TotalTimes = 1 }
            };
            var expectedStars = new List<int>() { 2, 4 };

            var combinationsSummary = new CombinationsSummary()
            {
                ColumnsSummary = columnsSummary,
                StarsSummary = starsSummary
            };

            //Act
            var actual = sut.GetCombinationWithMostCommonNumbers(combinationsSummary);

            //Assert
            CollectionAssert.AreEquivalent(expectedColumns, actual.Columns.ToList());
            CollectionAssert.AreEquivalent(expectedStars, actual.Stars.ToList());
        }
    }
}
