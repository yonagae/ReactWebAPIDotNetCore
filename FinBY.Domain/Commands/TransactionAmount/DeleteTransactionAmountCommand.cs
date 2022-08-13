using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;

namespace FinBY.Domain.Commands
{
    public class DeleteTransactionAmountCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public DeleteTransactionAmountCommand(int transactionAmountId)
        {
            TransactionAmountId = transactionAmountId;
        }

        public int TransactionAmountId { get; }

        public void Validate()
        {

        }
    }
}
