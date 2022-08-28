using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;

namespace FinBY.Domain.Commands
{
    public class CreateTransactionAmountCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public CreateTransactionAmountCommand(TransactionAmount transactionAmount)
        {
            TransactionAmount = transactionAmount;
        }

        public TransactionAmount TransactionAmount { get; }

        public void Validate()
        {

        }
    }
}
