using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET8.Showcase.Infrastructure.Context
{
    internal class ShowcaseContextFactory : IDesignTimeDbContextFactory<ShowcaseContext>
    {
        public ShowcaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShowcaseContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");

            return new ShowcaseContext(optionsBuilder.Options);
        }


    }
}
