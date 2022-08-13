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
    public class UpdateTransactionTypeHandler : IRequestHandler<UpdateTransactionTypeCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionTypeRepository _transactionTypeRepository { get; }

        public UpdateTransactionTypeHandler(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await _transactionTypeRepository.UpdateAsync(request.TransactionType);

            //if the data doesn't exist in the db
            if (result != null) return new GenericChangeCommandResult(false, null, result, true);

            return new GenericChangeCommandResult(true, null, result);
        }

    }
}
