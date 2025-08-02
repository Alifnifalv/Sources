using Eduegate.Integrations.Engine.DbContexts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts
{
    public class IntegrationDbContext : DbContext
    {
        public IntegrationDbContext()
        {
            
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Parent> Parent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["dbEduegateIntegrationDbContext"].ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
