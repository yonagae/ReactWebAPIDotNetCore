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
    public class UpdateTransactionAmountHandler : IRequestHandler<UpdateTransactionAmountCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionAmountRepository _transactionAmountRepository { get; }

        public UpdateTransactionAmountHandler(ITransactionAmountRepository transactionAmountRepository)
        {
            _transactionAmountRepository = transactionAmountRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            //TransactionAmountConverter conv = new TransactionAmountConverter();
            //var result = await _transactionAmountRepository.UpdateAsync(conv.Parse(request.TransactionAmount));

            ////if the data doesn't exist in the db
            //if (result != null) return new GenericChangeCommandResult(false, "", result, true);

            //return new GenericChangeCommandResult(true, "", result);

            return null;
        }

    }
}
