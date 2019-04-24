using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Model.Interfaces
{
    public interface ICombination
    {
        IEnumerable<int> Columns { get; set; }
        IEnumerable<int> Stars { get; set; }

    }
}
