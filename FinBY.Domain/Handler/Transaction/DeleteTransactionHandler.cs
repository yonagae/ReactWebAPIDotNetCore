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
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionRepository _transactionRepository { get; }

        public DeleteTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var result = await _transactionRepository.DeleteAsync(request.TransactionId);

            if (!result) return new GenericChangeCommandResult(false, new List<string>() { "Data not found" }, result, true);

            return new GenericChangeCommandResult(true, null, result);
        }

    }
}
