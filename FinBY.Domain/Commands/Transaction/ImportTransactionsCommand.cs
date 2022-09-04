using MediatR;
using FinBY.Domain.Commands.Interfaces;
using FinBY.Domain.Entities;
using System;
using System.Data;

namespace FinBY.Domain.Commands
{
    public class ImportTransactionsCommand : ICommand, IRequest<GenericChangeCommandResult>
    {

        public ImportTransactionsCommand(DataSet dataSet, DateTime dateTime)
        {
            DataSet = dataSet;
            DateTime = dateTime;
        }

        public DataSet DataSet { get; }
        public DateTime DateTime { get; }

        public void Validate()
        {

        }
    }
}
