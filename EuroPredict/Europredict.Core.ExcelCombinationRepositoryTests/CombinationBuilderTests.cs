using EuroPredict.Core.ExcelCombinationRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Europredict.Core.ExcelCombinationRepositoryTests
{
    [TestClass]
    public class CombinationBuilderTests
    {
        [TestMethod]
        [DataRow(";14 ;2019/027;2-abr.;9;26;16;2;36;6;7;QJB23561;;27 ;3;;;")]
        public void CreateCombinationTest_ValidInputs_ExpectedItems(string itemArray)
        {
            //Setup
            var items = itemArray.Split(";");
            var columns = items.Where((item, index) => index >= 4 && index <= 8).Select(item => Convert.ToInt32(item)).ToList();
            var stars = items.Where((item, index) => index >= 9 && index <= 10).Select(item => Convert.ToInt32(item)).ToList();

            //Act
            var actual = CombinationBuilder.CreateCombination(items, 4);

            //Assert
            Assert.IsNotNull(actual);
            CollectionAssert.AreEqual(columns, actual.Columns.ToList());
            CollectionAssert.AreEqual(stars, actual.Stars.ToList());
        }

        [TestMethod]
        [DataRow(";17 ;2019/033;23-abr.;;;;;;;;;;33 ;2;;;")]
        public void CreateCombinationTest_ValidInputs_ExpectedNoItems(string itemArray)
        {
            //Setup
            var items = itemArray.Split(";");

            //Act
            var actual = CombinationBuilder.CreateCombination(items, 4);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Columns.Count());
            Assert.AreEqual(0, actual.Stars.Count());
        }
    }
}
