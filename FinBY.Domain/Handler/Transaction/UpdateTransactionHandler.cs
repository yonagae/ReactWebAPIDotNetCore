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
        private IUnitOfWork _unitOfWork { get; }

        public UpdateTransactionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.Transaction.Validate();

            if (validationResult.isValid)
            {
                var result = await _unitOfWork.TransactionRepository.GetByIdAsync(request.Transaction.Id);
                if (result == null) 
                    return new GenericChangeCommandResult(false, new List<string>() { "Transaction not found" }, result, true);

                _unitOfWork.TransactionRepository.Update(request.Transaction);
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
