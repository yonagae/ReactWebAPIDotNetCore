using FinBY.Domain.Commands;
using FinBY.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FinBY.Domain.Data;

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
            var validationResult = request.Transaction.Validate();

            if (validationResult.isValid)
            {
                var result = await _transactionRepository.InsertAsync(request.Transaction);
                return new GenericChangeCommandResult(true, null, result);
            }
            else
            {
                return new GenericChangeCommandResult(false, validationResult.errorMessages, null);
            }
        }
    }
}
