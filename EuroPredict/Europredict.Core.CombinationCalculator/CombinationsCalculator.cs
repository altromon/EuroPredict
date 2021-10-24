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

        public ICombination GetMostProbableCombinationFromNumber(IEnumerable<ICombination> combinations, IEnumerable<int> containedColumns, IEnumerable<int> containedStars)
        {
            var restrictedCombinations = combinations.Where(combination => IsContained(combination.Columns, containedColumns) && IsContained(combination.Stars, containedStars));
            return GetCombinationWithMostCommonNumbers(restrictedCombinations);
        }

        public ICombination GetMostProbableCombinationFromNumber(IEnumerable<ICombination> combinations, IEnumerable<int> containedColumns)
        {
            return GetMostProbableCombinationFromNumber(combinations, containedColumns, containedStars: null);
        }

        private bool IsContained(IEnumerable<int> containerCollection, IEnumerable<int> containedCollection)
        {
            if (containedCollection == null) return true;
            if (containedCollection.Count() == 0) return true;

            var containerHashSet = new HashSet<int>(containerCollection);
            var containedHashSet = new HashSet<int>(containedCollection);

            return containerHashSet.IsSupersetOf(containedHashSet);
        }
    }
}
