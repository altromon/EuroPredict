using EuroPredict.Core.CombinationCalculator;
using EuroPredict.Core.ExcelCombinationRepository;
using Microsoft.Extensions.Configuration;
using EuroPredict.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EuroPredict.UI.SimpleConsole
{
    class Program
    {
        static void Main()
        {
            IConfigurationRoot configuration = LoadConfiguration();

            var combinationsDatasource = configuration.GetSection("CombinationsDatasource").Value;
            var repository = new CombinationRepository()
            {
                DataSource = combinationsDatasource
            };
            var combinations = repository.GetCombinations();
            var combinationsCalculator = new CombinationsCalculator();
            var combination = combinationsCalculator.GetCombinationWithMostCommonNumbers(combinations);

            Console.WriteLine($"Columns: {combination.Columns.ToSortedString()}");
            Console.WriteLine($"Stars: {combination.Stars.ToSortedString()}");

            Console.ReadLine();
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
