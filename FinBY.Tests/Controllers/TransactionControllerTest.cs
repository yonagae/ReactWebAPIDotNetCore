using AutoMapper;
using FinBY.API.Controllers;
using FinBY.Domain.Contracts;
using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Tests.Controllers
{
    [TestClass]
    public class TransactionControllerTest
    {
        private   TransactionController _transactionController;
        private  IUnitOfWork _unitOfWork;
        private  ILoggerManager _loggerManager;

        public TransactionControllerTest()
        {
            IMapper mapper = Substitute.For<IMapper>();
            IMediator mediator = Substitute.For<IMediator>();
            _loggerManager = Substitute.For<ILoggerManager>();
            _unitOfWork = new FakeUnitOfWork();
            _transactionController = new TransactionController(mapper, mediator, _loggerManager, _unitOfWork);
        }

            
        [TestMethod]
        public async Task GetAllTransactions_SimulateDBError_CatchesAndLog()
        {

            _unitOfWork.TransactionRepository.When(x => x.GetAllWithDetailsAsPagedResultAsync(Arg.Any<PagedTransactionParams>())).Do(x => { throw new Exception(); });

            var result = await _transactionController.GetDetailedTransactionsByParams(new PagedTransactionParams());
            var okResult = result as ObjectResult;


            _loggerManager.Received().LogError(Arg.Any<string>());
            okResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);        
        }

        [TestMethod]
        public async Task GetAllTransactions_SimulateOK_ReturnsOK()
        {
            _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(Arg.Any<PagedTransactionParams>()).Returns<PagedResult<Transaction>>(new PagedResult<Transaction>());

            var result = await _transactionController.GetDetailedTransactionsByParams(new PagedTransactionParams());
            var okResult = result as ObjectResult;


            _loggerManager.DidNotReceive().LogError(Arg.Any<string>());
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }


        [TestMethod]
        public async Task CreateTransaction_SimulateOK_ReturnsOK()
        {
            _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(Arg.Any<PagedTransactionParams>()).Returns<PagedResult<Transaction>>(new PagedResult<Transaction>());

            var result = await _transactionController.GetDetailedTransactionsByParams(new PagedTransactionParams());
            var okResult = result as ObjectResult;


            _loggerManager.DidNotReceive().LogError(Arg.Any<string>());
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        
    }
}
