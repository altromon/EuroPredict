using EuroPredict.Model.Interfaces;
using EuroPredict.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Core.CSVCombinationRepository
{
    public class CombinationFactory : ICombinationFactory
    {
        public ICombination CreateCombination(string line)
        {
            var combination = new Combination();

            var lineItems = line.Split(',');

            try
            {
                combination.Columns = new List<int>()
                {
                    int.Parse(lineItems[1]),
                    int.Parse(lineItems[2]),
                    int.Parse(lineItems[3]),
                    int.Parse(lineItems[4]),
                    int.Parse(lineItems[5]),
                };

                combination.Stars = new List<int>()
                {
                    int.Parse(lineItems[7]),
                    int.Parse(lineItems[8])
                };
            }
            catch(Exception)
            {
                combination = null;
            }

            return combination;
        }
    }
}
