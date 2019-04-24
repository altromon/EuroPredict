using System;
using System.Collections.Generic;
using System.Text;

namespace EuroPredict.Model.Interfaces
{
    interface Combination
    {
        IEnumerable<int> Columns { get; set; }
        IEnumerable<int> Stars { get; set; }

    }
}
