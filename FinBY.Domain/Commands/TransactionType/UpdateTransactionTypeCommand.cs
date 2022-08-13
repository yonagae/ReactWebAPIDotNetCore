using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;

namespace FinBY.Domain.Commands
{
    public class UpdateTransactionTypeCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public UpdateTransactionTypeCommand(TransactionType transactionType)
        {
            TransactionType = transactionType;
        }

        public TransactionType TransactionType { get; }

        public void Validate()
        {

        }
    }
}
