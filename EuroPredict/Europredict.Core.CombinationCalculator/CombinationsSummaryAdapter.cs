using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroPredict.Core.CombinationCalculator
{
    public class CombinationsSummaryAdapter
    {
        public IEnumerable<ICombination> Combinations { get; set; }

        public ICombinationsSummary GetCombinationsSummary()
        {
            var plainColumns = Combinations.SelectMany(combination => combination.Columns);
            var plainStars = Combinations.SelectMany(combination => combination.Stars);

            return new CombinationsSummary()
            {
                ColumnsSummary = SummarizeCollection(plainColumns),
                StarsSummary = SummarizeCollection(plainStars)
            };
        }

        public IEnumerable<INumberInfo> SummarizeCollection(IEnumerable<int> collection)
        {
            var groupedItems = collection.GroupBy(item => item)
                                    .Select(group => new NumberInfo { Number = group.Key, TotalTimes = group.Count() });
            return groupedItems;
        }
    }
}
