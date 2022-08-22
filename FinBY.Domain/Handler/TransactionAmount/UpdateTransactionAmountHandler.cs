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
    public class UpdateTransactionAmountHandler : IRequestHandler<UpdateTransactionAmountCommand, GenericChangeCommandResult>
    {
        private IUnitOfWork _unitOfWork { get; }

        public UpdateTransactionAmountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.TransactionAmount.Validate();

            if (validationResult.isValid)
            {
                var oldTransactionAmount = await _unitOfWork.TransactionAmountRepository.GetByIdAsync(request.TransactionAmount.Id);
                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionAmount.TransactionId);

                transaction.RemoveTransactionAmount(oldTransactionAmount);
                transaction.AddTransactionAmount(request.TransactionAmount);

                _unitOfWork.TransactionAmountRepository.Update(request.TransactionAmount);
                _unitOfWork.TransactionRepository.Update(transaction);

                await _unitOfWork.SaveAsync();

                return new GenericChangeCommandResult(true, null, request.TransactionAmount);
            }
            else
            {
                return new GenericChangeCommandResult(false, validationResult.errorMessages, null);
            }
        }

    }
}
