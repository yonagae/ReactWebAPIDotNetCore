using FinBY.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.DBTests
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
}
