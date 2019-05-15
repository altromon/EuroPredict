using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Europredict.Core.CombinationCalculator
{
    public class Combination : ICombination
    {
        public IEnumerable<int> Columns { get; set; }
        public IEnumerable<int> Stars { get; set; }
    }
}
