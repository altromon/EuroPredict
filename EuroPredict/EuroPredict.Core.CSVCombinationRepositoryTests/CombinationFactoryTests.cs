using EuroPredict.Core.CSVCombinationRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroPredict.Core.CSVCombinationRepositoryTests
{
    [TestClass]
    public class CombinationFactoryTests
    {
        CombinationFactory sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new CombinationFactory();
        }

        [DataTestMethod]
        [DataRow("22/10/2021,12,17,22,33,39,,02,12")]
        [DataRow("19/10/2021,04,20,36,40,41,,01,06")]
        [DataRow("15/10/2021,21,26,31,34,49,,02,05")]
        [DataRow("12/10/2021,06,13,22,45,49,,10,11")]
        [DataRow("8/10/2021,01,10,23,42,46,,03,05")]
        [DataRow("5/10/2021,11,13,14,36,45,,07,09")]
        [DataRow("1/10/2021,01,15,19,26,48,,06,12")]
        [DataRow("28/09/2021,06,07,12,33,39,,01,07")]
        [DataRow("24/09/2021,02,12,20,27,41,,10,12")]
        [DataRow("21/09/2021,20,25,26,30,38,,02,08")]
        [DataRow("17/09/2021,05,07,08,10,34,,01,09")]
        [DataRow("14/09/2021,12,18,35,38,45,,02,08")]
        [DataRow("10/09/2021,01,06,07,18,28,,02,08")]
        [DataRow("7/09/2021,07,19,35,42,43,,07,09")]
        [DataRow("3/09/2021,05,07,13,29,35,,03,07")]
        [DataRow("31/08/2021,05,13,17,32,43,,02,10")]
        [DataRow("27/08/2021,02,03,31,46,50,,08,12")]
        [DataRow("24/08/2021,19,21,36,44,50,,03,10")]
        [DataRow("20/08/2021,03,09,19,20,23,,09,11")]
        [DataRow("17/08/2021,12,31,41,42,47,,04,06")]
        [DataRow("13/08/2021,06,12,44,47,49,,08,12")]
        [DataRow("10/08/2021,09,37,47,48,49,,02,07")]
        [DataRow("6/08/2021,07,14,21,26,32,,04,12")]
        public void CreateCombinationTest_ValidData_ExpectedCombination(string line)
        {
            //Act
            var combination = sut.CreateCombination(line);

            //Assert
            var lineParts = line.Split(',');
            Assert.AreEqual(int.Parse(lineParts[1]), combination.Columns.ElementAt(0));
            Assert.AreEqual(int.Parse(lineParts[2]), combination.Columns.ElementAt(1));
            Assert.AreEqual(int.Parse(lineParts[3]), combination.Columns.ElementAt(2));
            Assert.AreEqual(int.Parse(lineParts[4]), combination.Columns.ElementAt(3));
            Assert.AreEqual(int.Parse(lineParts[5]), combination.Columns.ElementAt(4));
            Assert.AreEqual(int.Parse(lineParts[7]), combination.Stars.ElementAt(0));
            Assert.AreEqual(int.Parse(lineParts[8]), combination.Stars.ElementAt(1));
        }
    }
}
