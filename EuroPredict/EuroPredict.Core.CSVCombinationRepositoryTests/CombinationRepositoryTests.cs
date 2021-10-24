using EuroPredict.Core.CSVCombinationRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EuroPredict.Core.CSVCombinationRepositoryTests
{
    [TestClass]
    [DeploymentItem("./Assets/data.csv", "./Assets")]
    public class CombinationRepositoryTests
    {
        CombinationRepository sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new CombinationRepository
            {
                CombinationFactory = new DummyCombinationFactory()
            };
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalSourceData.TestData), typeof(ExternalSourceData), DynamicDataSourceType.Method)]
        public void GetCSV_ExistingItems_ExpectedData(string dataPath)
        {
            //Setup
            sut.DataSource = dataPath;

            //Act
            var actual = sut.GetCSV();

            //Assert
            Assert.IsTrue(actual.Count() > 0);
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalSourceData.TestData), typeof(ExternalSourceData), DynamicDataSourceType.Method)]
        public void GetStreamTest_ExistingItems_ExpectedData(string excelPath)
        {
            //Setup
            sut.DataSource = excelPath;

            //Act
            var actual = sut.GetStream();

            //Assert
            Assert.IsNotNull(actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalSourceData.TestData), typeof(ExternalSourceData), DynamicDataSourceType.Method)]
        public void GetCombinationsTest_ExistingItems_ExpectedData(string excelPath)
        {
            //Setup
            sut.DataSource = excelPath;

            //Act
            var actual = sut.GetCombinations();

            //Assert
            Assert.IsTrue(actual.Count() > 0);
            Assert.IsTrue(actual.All(combination => combination.Columns.All(column => column == 1) && combination.Stars.All(star => star == 1)));
            Console.WriteLine(actual.Count());
        }
    }
}
