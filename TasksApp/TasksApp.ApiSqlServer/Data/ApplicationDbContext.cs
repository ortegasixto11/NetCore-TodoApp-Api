using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp.ApiSqlServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(GetConnectionString()).Options)
        {
        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        public virtual DbSet<Models.Task> Tasks { get; set; }
    }
}
