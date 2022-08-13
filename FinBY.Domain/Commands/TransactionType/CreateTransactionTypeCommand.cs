﻿using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;
using FinBY.Domain.Data.DTO;

namespace FinBY.Domain.Commands
{
    public class CreateTransactionTypeCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public CreateTransactionTypeCommand(TransactionType transactionType)
        {
            TransactionType = transactionType;
        }

        public TransactionType TransactionType { get; }

        public void Validate()
        {

        }
    }
}
