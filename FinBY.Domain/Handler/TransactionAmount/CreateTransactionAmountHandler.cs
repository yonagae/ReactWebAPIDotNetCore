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
    public class CreateTransactionAmountHandler : IRequestHandler<CreateTransactionAmountCommand, GenericChangeCommandResult>
    {
        private IUnitOfWork _unitOfWork { get; }

        public CreateTransactionAmountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.TransactionAmount.Validate();

            if (validationResult.isValid)
            {
                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionAmount.TransactionId);
                transaction.AddTransactionAmount(request.TransactionAmount);

                _unitOfWork.TransactionAmountRepository.Add(request.TransactionAmount);
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
