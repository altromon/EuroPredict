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
            return Enumerable.Empty<ICombination>();
        }

        public DataSet GetExcelDataSet()
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = GetStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet();
                    /*
                    // Ejemplos de acceso a datos
                    DataTable table = result.Tables[0];
                    DataRow row = table.Rows[0];
                    string cell = row[0].ToString();*/
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
