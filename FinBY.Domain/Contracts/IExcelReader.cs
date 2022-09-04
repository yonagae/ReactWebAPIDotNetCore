using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Contracts
{
    public interface IExcelReader
    {
        public DataSet ReadExcelToDataTable(string filePath);
    }
}
