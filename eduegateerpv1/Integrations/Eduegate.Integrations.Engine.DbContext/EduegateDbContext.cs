using Eduegate.Integrations.Engine.DbContexts.EduegateModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts
{
    public class EduegateDbContext : DbContext
    {
        public EduegateDbContext()
        {

        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Relegion> Religion { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["dbEduegateDbContext"].ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
