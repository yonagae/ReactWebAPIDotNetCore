using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sproom.Web.Models;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Enum;
using SproomInbox.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sproom.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IDocumentRepository _documentRepo;
        private readonly IDocumentStateRepository _documentStateRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepo, IDocumentRepository documentRepo, IDocumentStateRepository documentStateRepo)
        {
            _userRepo = userRepo;
            _logger = logger;
            _documentRepo = documentRepo;
            _documentStateRepo = documentStateRepo;
        }

        public IActionResult Index()
        {
            //_userRepo.Add(new User("Yoshiro Nagae"));
            //_userRepo.Add(new User("Belkiss Martins"));
            //_userRepo.Add(new User("Morning Star"));
            //_userRepo.SaveChanges();

            //_documentRepo.Add(new Document("c:/aaa", eDocumentType.Invoice, eState.Approved, DateTime.Now));
            //_documentRepo.Add(new Document("c:/bbb", eDocumentType.CreditNote, eState.Received, DateTime.Now));
            //_documentRepo.Add(new Document("c:/ccc", eDocumentType.Invoice, eState.Rejected, DateTime.Now));
            //_documentRepo.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
