using EuroPredict.Core.CombinationCalculator;
//using EuroPredict.Core.ExcelCombinationRepository;
using Microsoft.Extensions.Configuration;
using EuroPredict.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EuroPredict.Core.CSVCombinationRepository;
using EuroPredict.Model.Interfaces;
using EuroPredict.UI.SimpleConsole.Properties;

namespace EuroPredict.UI.SimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = LoadConfiguration();

            var combinationsDatasource = configuration.GetSection("CombinationsDatasource").Value;
            var repository = new CombinationRepository()
            {
                DataSource = combinationsDatasource,
                CombinationFactory = new CombinationFactory()
            };
            var combinations = repository.GetCombinations();
            var combinationsCalculator = new CombinationsCalculator();
            ICombination combination = new Combination();
            if (args.Count() == 0)
            {
                combination = combinationsCalculator.GetCombinationWithMostCommonNumbers(combinations);
            }
            else
            {
                var columns = ConvertToIntCollection(args[0]);
                IEnumerable<int> stars = new List<int>();
                if (args.Count() > 1)
                {
                    stars = ConvertToIntCollection(args[1]);
                }
                combination = combinationsCalculator.GetMostProbableCombinationFromNumber(combinations, columns, stars);
            }

            if (combination.Columns.Count() == 0)
            {
                var columns = args[0];
                var stars = args.Length > 1 ? args[1] : string.Empty;
                Console.WriteLine(string.Format(Resources.NoCombination, columns, stars));
            }
            else
            { 
                Console.WriteLine($"Columns: {combination.Columns.ToSortedString()}");
                Console.WriteLine($"Stars: {combination.Stars.ToSortedString()}");
            }
        }

        private static IEnumerable<int> ConvertToIntCollection(string collectionString)
        {
            IEnumerable<int> res = new List<int>();

            try
            {
                res = collectionString.Split(',').Select(x => int.Parse(x)).ToList();
            }
            catch (Exception) { }

            return res;
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }
    }
}
