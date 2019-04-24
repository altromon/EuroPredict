using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Model.Interfaces
{
    public interface ICombinationRepository
    {
        string DataSource { get; set; }

        IEnumerable<ICombination> GetCombinations();
    }
}
