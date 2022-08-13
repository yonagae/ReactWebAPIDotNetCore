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
        private GenericChangeCommandResult result;
        public ITransactionAmountRepository _transactionAmountRepository { get; }

        public CreateTransactionAmountHandler(ITransactionAmountRepository transactionAmountRepository)
        {
            _transactionAmountRepository = transactionAmountRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            //TransactionAmountConverter conv = new TransactionAmountConverter();
            //var result = await _transactionAmountRepository.InsertAsync(conv.Parse(request.TransactionAmount));
            //return new GenericChangeCommandResult(true, "", result);
            return null;
        }

    }
}
