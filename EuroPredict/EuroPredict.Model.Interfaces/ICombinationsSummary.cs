using System.Collections.Generic;
using EuroPredict.Model.Interfaces;

namespace EuroPredict.Model.Interfaces
{
    public interface ICombinationsSummary
    {
        IEnumerable<INumberInfo> ColumnsSummary { get; set; }
        IEnumerable<INumberInfo> StarsSummary { get; set; }

        int GetColumnTotalTimes(int column);
        int GetStarTotalTimes(int star);
    }
}