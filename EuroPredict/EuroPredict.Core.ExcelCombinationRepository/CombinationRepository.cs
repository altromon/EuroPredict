using EuroPredict.Model.Interfaces;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EuroPredict.Core.ExcelCombinationRepository
{
    public class CombinationRepository : ICombinationRepository
    {
        public string DataSource { get; set; }

        public IEnumerable<ICombination> GetCombinations()
        {
            var excelDataSet = GetExcelDataSet();
            IEnumerable<ICombination> combinations = ExtractCombinations(excelDataSet);

            return combinations;
        }

        private IEnumerable<ICombination> ExtractCombinations(DataSet excelDataSet)
        {
            var combinations = new List<ICombination>();
            foreach (DataTable sheet in excelDataSet.Tables)
            {
                IEnumerable<ICombination> sheetCombinations = ExtractSheetCombinations(sheet);
                combinations.AddRange(sheetCombinations);
            }

            return combinations;
        }

        public IEnumerable<ICombination> ExtractSheetCombinations(DataTable sheet)
        {
            var combinations = new List<ICombination>();
            foreach (DataRow row in sheet.Rows)
            {
                bool canLoad = false;
                for(int i = 0; i< row.ItemArray.Length; i++)
                {
                    var item = row.ItemArray[i];
                    if(!(item is DBNull) && (item is DateTime))
                    {
                        canLoad = true;
                    }

                    if(canLoad)
                    {
                        ICombination combination = CombinationBuilder.CreateCombination(row.ItemArray, i+1);
                        combinations.Add(combination);
                        break;
                    }
                }
            }

            return combinations.Where(combination => combination.Columns.Count() > 0 && combination.Stars.Count() > 0);
        }

        public DataSet GetExcelDataSet()
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = GetStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet();
                }
            }
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
