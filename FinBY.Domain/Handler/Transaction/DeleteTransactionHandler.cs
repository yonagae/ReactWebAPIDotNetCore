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
        private IUnitOfWork _unitOfWork { get; }

        public DeleteTransactionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TransactionRepository.GetByIdAsync(request.TransactionId);

            if (result == null) return new GenericChangeCommandResult(false, new List<string>() { "Data not found" }, result, true);

            _unitOfWork.TransactionRepository.Remove(request.TransactionId);
            await _unitOfWork.SaveAsync();

            return new GenericChangeCommandResult(true, null, result);
        }

    }
}
