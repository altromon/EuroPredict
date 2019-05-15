using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Core.CombinationCalculator
{
    public class NumberInfo : INumberInfo
    {
        public int Number { get; set; }
        public int TotalTimes { get; set; }
    }
}
