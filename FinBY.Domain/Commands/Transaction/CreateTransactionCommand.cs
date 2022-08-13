using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;
using FinBY.Domain.Data.DTO;

namespace FinBY.Domain.Commands
{
    public class CreateTransactionCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public CreateTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }

        public Transaction Transaction { get; }

        public void Validate()
        {

        }
    }
}
