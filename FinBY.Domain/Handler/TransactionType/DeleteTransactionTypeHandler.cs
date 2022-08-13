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
    public class DeleteTransactionTypeHandler : IRequestHandler<DeleteTransactionTypeCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionTypeRepository _transactionTypeRepository { get; }

        public DeleteTransactionTypeHandler(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _transactionTypeRepository.DeleteAsync(request.TransactionTypeId);

            if (!result)  return new GenericChangeCommandResult(false, new List<string>() { "Data not found" }, result, true);

            return new GenericChangeCommandResult(true, null, result);
        }

    }
}
