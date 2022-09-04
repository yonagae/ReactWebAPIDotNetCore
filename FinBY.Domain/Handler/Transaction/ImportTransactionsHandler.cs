using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
using FinBY.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinBY.Domain.Handler
{
    public class ImportTransactionsHandler : IRequestHandler<ImportTransactionsCommand, GenericChangeCommandResult>
    {
        private IUnitOfWork _unitOfWork { get; }

        public ImportTransactionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(ImportTransactionsCommand request, CancellationToken cancellationToken)
        {
            var transactionList = new List<Transaction>();
            var ds = request.DataSet;
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++) //start with 1 to jump the header column
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    var date = DateTime.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString());
                    var description = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    var amount = Convert.ToDecimal(ds.Tables[0].Rows[i].ItemArray[3]);

                    if (request.DateTime < date) continue;

                    eTransactionFlow flow = eTransactionFlow.Credit;
                    if (amount < 0)
                    {
                        amount = Math.Abs(amount);
                        flow = eTransactionFlow.Debit;
                    }

                    var transaction = new Transaction(flow, 1, 1, date, description, description);
                    transaction.AddAmount(new TransactionAmount(0, 1, amount));
                    transactionList.Add(transaction);
                }
            }

            _unitOfWork.TransactionRepository.Add(transactionList);
            await _unitOfWork.TransactionRepository.SaveChangesAsync();

            return new GenericChangeCommandResult(true, null, transactionList);
        }
    }
}
