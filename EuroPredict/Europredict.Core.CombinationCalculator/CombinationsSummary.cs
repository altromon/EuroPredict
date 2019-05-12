using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Europredict.Core.CombinationCalculator
{
    public class CombinationsSummary : ICombinationsSummary
    {
        public IEnumerable<INumberInfo> ColumnsSummary { get; set; }
        public IEnumerable<INumberInfo> StarsSummary { get; set; }

        public int GetColumnTotalTimes(int column)
        {
            return GetCollectionValueTotalTimes(ColumnsSummary, column);
        }

        public int GetCollectionValueTotalTimes(IEnumerable<INumberInfo> collection, int value)
        {
            return collection?.FirstOrDefault(item => item.Number == value)?.TotalTimes ?? 0;
        }

        public int GetStartTotalTimes(int star)
        {
            return GetCollectionValueTotalTimes(StarsSummary, star);
        }
    }
}
