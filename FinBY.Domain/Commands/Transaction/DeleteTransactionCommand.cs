using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;

namespace FinBY.Domain.Commands
{
    public class DeleteTransactionCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public DeleteTransactionCommand(int transactionId)
        {
            TransactionId = transactionId;
        }

        public int TransactionId { get; }

        public void Validate()
        {

        }
    }
}
