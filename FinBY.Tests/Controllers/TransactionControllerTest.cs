using AutoMapper;
using FinBY.API.Controllers;
using FinBY.Domain.Contracts;
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
        [TestMethod]
        public void GetAllTransactions_SimulateDBError_CatchesAndLog()
        {
            IMapper mapper = Substitute.For<IMapper>();
            IMediator mediator = Substitute.For<IMediator>();
            ILoggerManager loggerManager = Substitute.For<ILoggerManager>();
            IRepositoryWrapper repositoryWrapper = new FakeRepositoryWrapper();
            TransactionController transactionController = new TransactionController(mapper, mediator, loggerManager, repositoryWrapper);
            repositoryWrapper.TransactionRepository.When(x => x.GetAllWithDetailsAsList()).Do(x => { throw new Exception(); });

            var result = transactionController.GetAllTransactions();
            var okResult = result.Result as ObjectResult;


            loggerManager.Received().LogError(Arg.Any<string>());
            okResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);        
        }

        [TestMethod]
        public void GetAllTransactions_SimulateOK_ReturnsOK()
        {
            IMapper mapper = Substitute.For<IMapper>();
            IMediator mediator = Substitute.For<IMediator>();
            ILoggerManager loggerManager = Substitute.For<ILoggerManager>();
            IRepositoryWrapper repositoryWrapper = new FakeRepositoryWrapper();
            TransactionController transactionController = new TransactionController(mapper, mediator, loggerManager, repositoryWrapper);
            repositoryWrapper.TransactionRepository.GetAllWithDetailsAsList().Returns<List<Transaction>>(new List<Transaction>());

            var result = transactionController.GetAllTransactions();
            var okResult = result.Result as ObjectResult;


            loggerManager.DidNotReceive().LogError(Arg.Any<string>());
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }


        [TestMethod]
        public void CreateTransaction_SimulateOK_ReturnsOK()
        {
            IMapper mapper = Substitute.For<IMapper>();
            IMediator mediator = Substitute.For<IMediator>();
            ILoggerManager loggerManager = Substitute.For<ILoggerManager>();
            IRepositoryWrapper repositoryWrapper = new FakeRepositoryWrapper();
            TransactionController transactionController = new TransactionController(mapper, mediator, loggerManager, repositoryWrapper);
            repositoryWrapper.TransactionRepository.GetAllWithDetailsAsList().Returns<List<Transaction>>(new List<Transaction>());

            var result = transactionController.GetAllTransactions();
            var okResult = result.Result as ObjectResult;


            loggerManager.DidNotReceive().LogError(Arg.Any<string>());
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        
    }
}
