using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroPredict.Core.CombinationCalculator
{
    public class CombinationsCalculator
    {
        public IEnumerable<INumberInfo> SortMostProbableNumberInfos(IEnumerable<INumberInfo> numberInfos)
        {
            return numberInfos?.OrderByDescending(numberInfo => numberInfo.TotalTimes);
        }

        public ICombination GetCombinationWithMostCommonNumbers(ICombinationsSummary CombinationsSummary)
        {
            var sortedColumns = SortMostProbableNumberInfos(CombinationsSummary.ColumnsSummary);
            var sortedStars = SortMostProbableNumberInfos(CombinationsSummary.StarsSummary);

            return new Combination()
            {
                Columns = sortedColumns?.Take(5)?.Select(numberInfo => numberInfo.Number),
                Stars = sortedStars?.Take(2)?.Select(numberInfo => numberInfo.Number),
            };
        }

        public ICombination GetCombinationWithMostCommonNumbers(IEnumerable<ICombination> combinations)
        {
            var adapter = new CombinationsSummaryAdapter()
            {
                Combinations = combinations
            };

            return GetCombinationWithMostCommonNumbers(adapter.GetCombinationsSummary());
        }

        public ICombination GetMostProbableCombinationFromNumber(IEnumerable<ICombination> combinations, int startValue)
        {
            //TODO: develop method to return the combination which uses the start value the more times, then the second value is that which appears the most with the first and so on.
            return null;
        }
    }
}
