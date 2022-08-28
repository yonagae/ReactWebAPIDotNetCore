using FinBY.Domain.Entities;
using FinBY.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.DBTests
{
    [TestClass]
    public class TestDatabaseFixture
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        private static TestApplicationDbContext _Instance = null;
        public static TestApplicationDbContext Instance
        {
            get
            {
                return _Instance;
            }
        }

        [AssemblyInitialize]
        public static void Initialize(TestContext context123)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    _Instance = new TestApplicationDbContext();
                    _Instance.Database.EnsureDeleted();
                    _Instance.Database.EnsureCreated();

                    _Instance.AddRange(
                        new User("Jonh Main", "main", "24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9", "h9lzVOoLlBoTbcQrh/e16/aIj+4p6C67lLdDbBRMsjE=", DateTime.Now.AddYears(1)),
                        new User("Batman", "batman", "24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9", "h9lzVOoLlBoTbcQrh/e16/aIj+4p6C67lLdDbBRMsjE=", DateTime.Now.AddYears(1))
                       );

                    _Instance.AddRange(
                      new TransactionType("Casa"),
                      new TransactionType("Mercado"),
                      new TransactionType("Pessoal"),
                      new TransactionType("Luz"),
                      new TransactionType("Agua"));

                    _Instance.SaveChanges();

                    _databaseInitialized = true;
                }
            }
        }


        [AssemblyCleanup]
        public static void CleanUp()
        {
            _Instance.Dispose();
        }
    }
}
