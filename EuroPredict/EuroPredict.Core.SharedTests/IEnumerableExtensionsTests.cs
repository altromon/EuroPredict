using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using EuroPredict.Core.Shared;

namespace EuroPredict.Core.SharedTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ToSortedStringTests_ValidInput_ExpectedSorting()
        {
            //Setup
            var input = new List<int>() { 5, 1, 2, 4 };

            //Act
            var actual = input.ToSortedString();

            //Assert
            Assert.AreEqual("1,2,4,5", actual);
        }
    }
}
