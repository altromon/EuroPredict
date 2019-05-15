using EuroPredict.Core.ExcelCombinationRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EuroPredict.Core.ExcelCombinationRepositoryTests
{
    [TestClass]
    [DeploymentItem("./Assets/HistoricoEuromillones.xls", "./Assets")]
    public class CombinationRepositoryTests
    {
        CombinationRepository sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new CombinationRepository();
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalExcelSourceData.TestData), typeof(ExternalExcelSourceData), DynamicDataSourceType.Method)]
        public void GetExcelDataSetTest_ExistingItems_ExpectedData(string excelPath)
        {
            //Setup
            sut.DataSource = excelPath;

            //Act
            var actual = sut.GetExcelDataSet();

            //Assert
            Assert.IsTrue(actual.Tables.Count > 0);
        }

        [DataTestMethod]
        [DynamicData(nameof(ExternalExcelSourceData.TestData), typeof(ExternalExcelSourceData), DynamicDataSourceType.Method)]
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
        [DynamicData(nameof(ExternalExcelSourceData.TestData), typeof(ExternalExcelSourceData), DynamicDataSourceType.Method)]
        public void GetCombinationsTest_ExistingItems_ExpectedData(string excelPath)
        {
            //Setup
            sut.DataSource = excelPath;

            //Act
            var actual = sut.GetCombinations();

            //Assert
            Assert.IsTrue(actual.Count() > 0);
            Console.WriteLine(actual.Count());
        }
    }
}
