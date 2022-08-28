using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.WebTests
{
    [SetUpFixture]
    public class MySetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Prep the database
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // tear down the database
        }
    }
}
