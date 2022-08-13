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

        [AssemblyInitialize]
        public static void Initialize(TestContext context123)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = new TestApplicationDbContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }
   }
}
