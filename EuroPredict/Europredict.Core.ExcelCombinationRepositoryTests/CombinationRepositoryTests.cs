using EuroPredict.Core.ExcelCombinationRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Europredict.Core.ExcelCombinationRepositoryTests
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
    }
}
