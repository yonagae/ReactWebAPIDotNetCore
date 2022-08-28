using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;

namespace FinBY.Domain.Commands
{
    public class UpdateTransactionAmountCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public UpdateTransactionAmountCommand(TransactionAmount transactionAmount)
        {
            TransactionAmount = transactionAmount;
        }

        public TransactionAmount TransactionAmount { get; }

        public void Validate()
        {

        }
    }
}
