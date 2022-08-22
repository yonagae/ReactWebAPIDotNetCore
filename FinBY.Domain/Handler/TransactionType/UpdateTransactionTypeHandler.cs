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
        private IUnitOfWork _unitOfWork { get; }

        public UpdateTransactionTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 

        public async Task<GenericChangeCommandResult> Handle(UpdateTransactionTypeCommand request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.TransactionTypeRepository.GetByIdAsync(request.TransactionType.Id);
            if (result != null) return new GenericChangeCommandResult(false, null, result, true);

            _unitOfWork.TransactionTypeRepository.Update(request.TransactionType);
            await _unitOfWork.SaveAsync();

            return new GenericChangeCommandResult(true, null, result);
        }

    }
}
