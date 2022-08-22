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
        private IUnitOfWork _unitOfWork { get; }

        public CreateTransactionTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericChangeCommandResult> Handle(CreateTransactionTypeCommand request, CancellationToken cancellationToken)
        { 
             _unitOfWork.TransactionTypeRepository.Add(request.TransactionType);
            await _unitOfWork.SaveAsync();

            return new GenericChangeCommandResult(true, null, null);
        }

    }
}
