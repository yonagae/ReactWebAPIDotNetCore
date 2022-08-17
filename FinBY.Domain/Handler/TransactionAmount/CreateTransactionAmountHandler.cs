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
        private readonly IRepositoryWrapper repositoryWrapper;

        public CreateTransactionAmountHandler(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        } 

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.TransactionAmount.Validate();

            if (validationResult.isValid)
            {
                var transaction = repositoryWrapper.TransactionRepository.GetById(request.TransactionAmount.TransactionId);
                transaction.AddTransactionAmount(request.TransactionAmount);

                repositoryWrapper.TransactionAmountRepository.Add(request.TransactionAmount);
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
