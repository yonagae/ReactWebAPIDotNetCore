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
            var result = await _transactionRepository.UpdateAsync(request.Transaction);

            //if the data doesn't exist in the db
            if (result != null) return new GenericChangeCommandResult(false, "", result, true);

            return new GenericChangeCommandResult(true, "", result);
        }

    }
}
