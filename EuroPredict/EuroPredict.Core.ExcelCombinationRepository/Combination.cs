using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace EuroPredict.Core.OnlineExcelCombinationRepository
{
    public class Combination : ICombination
    {
        public IEnumerable<int> Columns { get; set; }
        public IEnumerable<int> Stars { get; set; }
    }
}
