using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SproomInbox.Domain.Commands;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Enum;
using SproomInbox.Domain.Handlers;
using SproomInbox.Domain.Repositories;
using SproomInbox.Domain.Services;
using System;

namespace SproomInbox.Tests.Handler
{
    [TestClass]
    public class AproveDocumentCommandHandlerTest
    {

        private Document _document ;
        private IDocumentRepository _documentRepo;
        private IDocumentStateRepository _documentStateRepo;
        private IEmailService _emailService;
        private AproveDocumentCommand _command;
        private AproveDocumentCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _document = new Document(Guid.NewGuid(), "test.txt", eDocumentType.CreditNote, eState.Received, new DateTime(2022, 02, 01));
            _documentRepo = Substitute.For<IDocumentRepository>();
            _documentStateRepo = Substitute.For<IDocumentStateRepository>();
            _emailService = Substitute.For<IEmailService>();
            _command = new AproveDocumentCommand(_document.Id);
            _handler = new AproveDocumentCommandHandler(_documentRepo, _documentStateRepo, _emailService);
            _documentRepo.GetById(_document.Id).Returns<Document>(_document);
        }

        [TestMethod]
        public void Handle_AproveReceivedDocument_Aproves()
        {
            _document.State = eState.Received;
            _handler.Handle(_command, new System.Threading.CancellationToken());
            _document.State.Should().Be(eState.Approved);
            _documentStateRepo.Received().Add(Arg.Is<DocumentState>(x => x.Document.Id == _document.Id && x.State == eState.Approved));
            _emailService.Received().SendEmailAsync(Arg.Any<string>(), Arg.Any<string>());
        }
        [TestMethod]
        public void Handle_AproveRecejectedDocument_Aproves()
        {
            _document.State = eState.Rejected;
            _handler.Handle(_command, new System.Threading.CancellationToken());
            _document.State.Should().Be(eState.Approved);
            _documentStateRepo.Received().Add(Arg.Is<DocumentState>(x => x.Document.Id == _document.Id && x.State == eState.Approved));
            _emailService.Received().SendEmailAsync(Arg.Any<string>(), Arg.Any<string>());
        }
        [TestMethod]
        public void Handle_AproveApprovedDocument_DoesNothing()
        {
            _document.State = eState.Approved;
            _handler.Handle(_command, new System.Threading.CancellationToken());
            _document.State.Should().Be(eState.Approved);
            _documentStateRepo.DidNotReceive().Add(Arg.Any<DocumentState>());
            _emailService.DidNotReceive().SendEmailAsync(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
