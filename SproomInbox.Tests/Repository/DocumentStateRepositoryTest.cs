using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Enum;
using SproomInbox.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Tests.Repository
{
    [TestClass]
    public class DocumentStateRepositoryTest
    {

        DocumentStateRepository _documentStateRepo;
        Document _document;
        List<DocumentState> _list;

        [TestInitialize]
        public void Setup()
        {
            _document = new Document(Guid.NewGuid(), "test.txt", eDocumentType.CreditNote, eState.Approved, DateTime.Now);
            Document document2 = new Document(Guid.NewGuid(), "test2.txt", eDocumentType.CreditNote, eState.Approved, DateTime.Now);
            _list = new List<DocumentState>()
            {
                new DocumentState(Guid.NewGuid(), DateTime.Now, Domain.Enum.eState.Approved, _document),
                new DocumentState(Guid.NewGuid(), DateTime.Now, Domain.Enum.eState.Rejected, _document),
                new DocumentState(Guid.NewGuid(), DateTime.Now, Domain.Enum.eState.Received, _document),
                new DocumentState(Guid.NewGuid(), DateTime.Now, Domain.Enum.eState.Approved, document2),
                new DocumentState(Guid.NewGuid(), DateTime.Now, Domain.Enum.eState.Rejected, document2)
            };

            DbContext dbContext = Substitute.For<DbContext>();
            _documentStateRepo = Substitute.For<DocumentStateRepository>(dbContext);
            _documentStateRepo.GetAll().Returns<IQueryable<DocumentState>>(_list.AsQueryable());
        }

        [TestMethod]
        public void GetDocumentStatesByDocumentId_ExistingID_GetTheCorrectDocumentStates()
        {
           var listDocumentStates = _documentStateRepo.GetDocumentStatesByDocumentId(_document.Id);
            listDocumentStates.Count.Should().Be(3);
            listDocumentStates.Should().OnlyContain(x => x.Document.Id == _document.Id);
            listDocumentStates.Should().Contain(_list[0]);
            listDocumentStates.Should().Contain(_list[1]);
            listDocumentStates.Should().Contain(_list[2]);
        }

        [TestMethod]
        public void GetDocumentStatesByDocumentId_NonExistingID_ReturnEmpty()
        {
            var listDocumentStates = _documentStateRepo.GetDocumentStatesByDocumentId(Guid.NewGuid());
            listDocumentStates.Count.Should().Be(0);
        }
    }
}
