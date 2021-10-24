using EuroPredict.Model.Interfaces;

namespace EuroPredict.Core.CSVCombinationRepository
{
    public interface ICombinationFactory
    {
        ICombination CreateCombination(string line);
    }
}