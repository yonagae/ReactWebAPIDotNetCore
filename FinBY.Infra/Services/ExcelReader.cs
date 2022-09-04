using ExcelDataReader;
using FinBY.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Infra.Services
{
    public  class ExcelReader : IExcelReader
    {
        public DataSet ReadExcelToDataTable(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet();
                }
            }

            return null;
        }
    }
}
