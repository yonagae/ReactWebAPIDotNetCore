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
    public class DeleteTransactionAmountHandler : IRequestHandler<DeleteTransactionAmountCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionAmountRepository _transactionAmountRepository { get; }

        public DeleteTransactionAmountHandler(ITransactionAmountRepository transactionAmountRepository)
        {
            _transactionAmountRepository = transactionAmountRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionAmountCommand request, CancellationToken cancellationToken)
        {
            var result = await _transactionAmountRepository.DeleteAsync(request.TransactionAmountId);

            if (!result) return new GenericChangeCommandResult(false, "Data not found", result, true);

            return new GenericChangeCommandResult(true, "", result);
        }

    }
}
