using EuroPredict.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;

namespace EuroPredict.Core.CSVCombinationRepository
{
    public class CombinationRepository : ICombinationRepository
    {
        public string DataSource { get; set; }
        public ICombinationFactory CombinationFactory { get; set; }

        public IEnumerable<ICombination> GetCombinations()
        {
            var lines = GetCSV();
            IEnumerable<ICombination> combinations = ExtractCombinations(lines);

            return combinations;
        }
        
        private IEnumerable<ICombination> ExtractCombinations(IEnumerable<string> lines)
        {
            if (CombinationFactory == null) throw new ArgumentNullException(nameof(CombinationFactory));

            var combinations = new List<ICombination>();
            foreach (string line in lines)
            {
                ICombination combination = CombinationFactory.CreateCombination(line);
                if(combination != null) combinations.Add(combination);
            }

            return combinations;
        }

        public IEnumerable<string> GetCSV()
        {
            var lines = new List<string>();
            using (var stream =  new StreamReader(GetStream()))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    lines.Add(line);
                }
            }

            return lines;
        }

        public Stream GetStream()
        {
            if (string.IsNullOrWhiteSpace(DataSource)) throw new ArgumentNullException(DataSource);

            byte[] excelBytes = null;
            using (var client = new WebClient())
            {
                excelBytes = client.DownloadData(DataSource);
            }

            return new MemoryStream(excelBytes);
        }
    }
}
