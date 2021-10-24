using EuroPredict.Core.CSVCombinationRepository;
using EuroPredict.Model.Interfaces;
using EuroPredict.Model.Shared;
using System.Collections.Generic;

namespace EuroPredict.Core.CSVCombinationRepositoryTests
{
    internal class DummyCombinationFactory : ICombinationFactory
    {
        public ICombination CreateCombination(string line)
        {
            var combination = new Combination()
            {
                Columns = new List<int>() { 1, 1, 1, 1, 1 },
                Stars = new List<int>() { 1, 1 }
            };

            return combination;
        }
    }
}