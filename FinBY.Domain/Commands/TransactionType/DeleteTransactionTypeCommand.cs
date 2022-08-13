using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;

namespace FinBY.Domain.Commands
{
    public class DeleteTransactionTypeCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public DeleteTransactionTypeCommand(int transactionTypeId)
        {
            TransactionTypeId = transactionTypeId;
        }

        public int TransactionTypeId { get; }

        public void Validate()
        {

        }
    }
}
