using FinBY.Domain.Commands;
using FinBY.Domain.Data;
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
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionRepository _transactionRepository { get; }

        public UpdateTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.Transaction.Validate();

            if (validationResult.isValid)
            {
                var result = await _transactionRepository.UpdateAsync(request.Transaction);

                //if the data doesn't exist in the db
                if (result == null) return new GenericChangeCommandResult(false, new List<string>() { "Transaction not found" }, result, true);

                return new GenericChangeCommandResult(true, null, result);
            }
            else
            {
                return new GenericChangeCommandResult(false, validationResult.errorMessages, null);
            }
        }

    }
}
