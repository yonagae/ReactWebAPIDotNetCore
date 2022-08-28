using FinBY.API;
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
                    DBStartUp.StartupBase(Instance);
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
