using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
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
    public class DeleteTransactionAmountHandler : IRequestHandler<DeleteTransactionAmountCommand, GenericChangeCommandResult>
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        public DeleteTransactionAmountHandler(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        } 

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var transactionAmount = repositoryWrapper.TransactionAmountRepository.GetById(request.TransactionAmountId);

            if (transactionAmount == null) return new GenericChangeCommandResult(false, new List<string>() { "Transaction Amount not found" }, transactionAmount, true);

            var transaction = repositoryWrapper.TransactionRepository.GetById(transactionAmount.TransactionId);

            transaction.RemoveTransactionAmount(transactionAmount);

            this.repositoryWrapper.TransactionAmountRepository.Remove(request.TransactionAmountId);
            this.repositoryWrapper.TransactionRepository.Update(transaction);
            await this.repositoryWrapper.SaveAsync();

            return new GenericChangeCommandResult(true, null, null);
        }

    }
}
