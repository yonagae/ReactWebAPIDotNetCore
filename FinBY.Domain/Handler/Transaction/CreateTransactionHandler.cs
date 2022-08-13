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
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionRepository _transactionRepository { get; }

        public CreateTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            //var result = await _transactionRepository.InsertAsync(request.Transaction);
            return null;// new GenericChangeCommandResult(true, "", result);
        }

    }
}
