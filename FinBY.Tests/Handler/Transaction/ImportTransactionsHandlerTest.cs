using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
using FinBY.Infra.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Tests.Handler
{
    [TestClass]
    public  class ImportTransactionsHandlerTest
    {
        [TestMethod]
        public async Task Handle_Import()
        {
            var transactionList = new List<Transaction>();
            try
            {
                ExcelReader excelReader = new ExcelReader();
                var ds = excelReader.ReadExcelToDataTable(@"C:\Users\random\Downloads\Movimentos (3).xls");

                for (int i = 1; i < ds.Tables[0].Rows.Count; i++) //start with 1 to jump the header column
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        var date = DateTime.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                        var description = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                        var amount = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[3]);

                        eTransactionFlow flow = eTransactionFlow.Credit;
                        if (amount < 0) 
                            flow = eTransactionFlow.Debit;

                        var transaction = new Transaction(flow, 1, 1, date, description, description);
                        transaction.AddAmount(new TransactionAmount(0, 1, amount));
                        transactionList.Add(transaction);
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }
    }
}
