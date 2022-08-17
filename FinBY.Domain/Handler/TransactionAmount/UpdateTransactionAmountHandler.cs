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
        private readonly IRepositoryWrapper repositoryWrapper;

        public UpdateTransactionAmountHandler(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.TransactionAmount.Validate();

            if (validationResult.isValid)
            {
                var oldTransactionAmount = repositoryWrapper.TransactionAmountRepository.GetById(request.TransactionAmount.Id);
                var transaction = repositoryWrapper.TransactionRepository.GetById(request.TransactionAmount.TransactionId);

                transaction.RemoveTransactionAmount(oldTransactionAmount);
                transaction.AddTransactionAmount(request.TransactionAmount);

                repositoryWrapper.TransactionAmountRepository.Update(request.TransactionAmount);
                repositoryWrapper.TransactionRepository.Update(transaction);

                await repositoryWrapper.SaveAsync();

                return new GenericChangeCommandResult(true, null, request.TransactionAmount);
            }
            else
            {
                return new GenericChangeCommandResult(false, validationResult.errorMessages, null);
            }
        }

    }
}
