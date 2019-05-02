using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Core.ExcelCombinationRepository
{
    public static class CombinationBuilder
    {
        public static Combination CreateCombination(object[] itemArray, int initialIndex)
        {
            List<int> columns = ReadColumns(itemArray, initialIndex, 5);
            List<int> stars = ReadColumns(itemArray, initialIndex + 5, 2);
            return new Combination()
            {
                Columns = columns,
                Stars = stars
            };
        }

        public static List<int> ReadColumns(object[] sourceItemArray, int preInitialIndex, int totalItems)
        {
            var res = new List<int>();
            for (int i = 0; i < totalItems; i++)
            {
                var item = sourceItemArray[preInitialIndex + i];
                if (!(item is DBNull)) res.Add(Convert.ToInt32(item));
            }

            return res;
        }
    }
}
