using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Infrastructure.Database
{
    public class BuecherverwaltungDatenbankContextFactory : IDesignTimeDbContextFactory<BuecherverwaltungDatenbankContext>
    {
        public BuecherverwaltungDatenbankContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BuecherverwaltungDatenbankContext>();
            string connection = Environment.GetEnvironmentVariable("LIBARY_CONNECTION", EnvironmentVariableTarget.Machine);
            Console.WriteLine($"Connection: {connection}");
            optionsBuilder.UseSqlServer(connection);

            return new BuecherverwaltungDatenbankContext(optionsBuilder.Options);
  
        }
    }
}
