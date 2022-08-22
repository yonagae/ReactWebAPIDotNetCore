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
        private IUnitOfWork _unitOfWork { get; }

        public CreateTransactionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.Transaction.Validate();

            if (validationResult.isValid)
            {
                _unitOfWork.TransactionRepository.Add(request.Transaction);
                 await _unitOfWork.SaveAsync();
                return new GenericChangeCommandResult(true, null, request.Transaction);
            }
            else
            {
                return new GenericChangeCommandResult(false, validationResult.errorMessages, null);
            }
        }
    }
}
