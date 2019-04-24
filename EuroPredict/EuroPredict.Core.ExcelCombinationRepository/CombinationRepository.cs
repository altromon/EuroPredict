using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Core.OnlineExcelCombinationRepository
{
    public class CombinationRepository : ICombinationRepository
    {
        public string DataSource { get; set; }

        public IEnumerable<ICombination> GetCombinations()
        {
            throw new NotImplementedException();
        }
    }
}
