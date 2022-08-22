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
        private IUnitOfWork _unitOfWork { get; }

        public DeleteTransactionAmountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var transactionAmount = await _unitOfWork.TransactionAmountRepository.GetByIdAsync(request.TransactionAmountId);

            if (transactionAmount == null) return new GenericChangeCommandResult(false, new List<string>() { "Transaction Amount not found" }, transactionAmount, true);

            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(transactionAmount.TransactionId);

            transaction.RemoveTransactionAmount(transactionAmount);

            this._unitOfWork.TransactionAmountRepository.Remove(request.TransactionAmountId);
            this._unitOfWork.TransactionRepository.Update(transaction);
            await this._unitOfWork.SaveAsync();

            return new GenericChangeCommandResult(true, null, null);
        }

    }
}
