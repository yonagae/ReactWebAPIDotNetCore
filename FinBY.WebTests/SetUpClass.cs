using FinBY.API;
using FinBY.Infra.Context;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.WebTests
{
    public class TestApplicationDbContext : ApplicationDbContext
    {
        public TestApplicationDbContext() : base(null)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // define the database to use
            optionsBuilder.UseSqlServer("data source=DESKTOP-MMPFEV4\\SQLEXPRESS; Initial Catalog=FinBY4Test;Integrated Security=True;");
        }
    }

    [SetUpFixture]
    public class MySetUpClass
    {
        private static TestApplicationDbContext _Instance = null;
        public static TestApplicationDbContext Instance
        {
            get
            {
                return _Instance;
            }
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _Instance = new TestApplicationDbContext();
            DBStartUp.StartupBase(_Instance);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // tear down the database
        }
    }
}
