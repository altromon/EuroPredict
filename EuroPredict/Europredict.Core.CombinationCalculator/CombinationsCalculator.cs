using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Europredict.Core.CombinationCalculator
{
    public class CombinationsCalculator
    {
        public ICombinationsSummary CombinationsSummary { get; set; }
        public IEnumerable<ICombination> Combinations { get; set; }

        public IEnumerable<INumberInfo> SortMostProbableNumberInfos(IEnumerable<INumberInfo> numberInfos)
        {
            return numberInfos?.OrderByDescending(numberInfo => numberInfo.TotalTimes);
        }

        public ICombination GetCombinationWithMostCommonNumbers()
        {
            var sortedColumns = SortMostProbableNumberInfos(CombinationsSummary.ColumnsSummary);
            var sortedStars = SortMostProbableNumberInfos(CombinationsSummary.StarsSummary);

            return new Combination()
            {
                Columns = sortedColumns?.Take(5)?.Select(numberInfo => numberInfo.Number),
                Stars = sortedStars?.Take(2)?.Select(numberInfo => numberInfo.Number),
            };
        }
    }
}
