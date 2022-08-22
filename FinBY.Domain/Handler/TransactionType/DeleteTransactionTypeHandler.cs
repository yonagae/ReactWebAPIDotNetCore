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
        private IUnitOfWork _unitOfWork { get; }

        public DeleteTransactionTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(DeleteTransactionTypeCommand request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.TransactionTypeRepository.GetByIdAsync(request.TransactionTypeId);
            if (result != null) return new GenericChangeCommandResult(false, new List<string>() { "Data not found" }, result, true);

            _unitOfWork.TransactionTypeRepository.Remove(request.TransactionTypeId);
            await _unitOfWork.SaveAsync();

            return new GenericChangeCommandResult(true, null, result);
        }

    }
}
