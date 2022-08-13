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
    public class CreateTransactionTypeHandler : IRequestHandler<CreateTransactionTypeCommand, GenericChangeCommandResult>
    {
        private GenericChangeCommandResult result;
        public ITransactionTypeRepository _transactionTypeRepository { get; }

        public CreateTransactionTypeHandler(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        } 

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionTypeCommand request, CancellationToken cancellationToken)
        {
            //TransactionTypeConverter conv = new TransactionTypeConverter();
            //var result = await _transactionTypeRepository.InsertAsync(conv.Parse(request.TransactionType));

            return new GenericChangeCommandResult(true, "", null);
        }

    }
}
