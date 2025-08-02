using Eduegate.Domain.Entity.Models.Clinic;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    public partial class dbClinicContext: DbContext
    {
        public dbClinicContext()
        {
        }

        public dbClinicContext(DbContextOptions<dbClinicContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetBlinkConnectionString());
            }
        }

        public DbSet<AxDocument> AxDocuments { get; set; }
        public DbSet<AxImage> AxImages { get; set; }
        public DbSet<SL_Clinic> SL_Clinic { get; set; }
        public DbSet<SL_Department> SL_Department { get; set; }
        public DbSet<SLDoctor> SLDoctors { get; set; }
        public DbSet<DoctorClinicDepartment> DoctorClinicDepartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new AxDocumentMap());
            //modelBuilder.Configurations.Add(new AxImageMap());
            //modelBuilder.Configurations.Add(new SL_ClinicMap());
            //modelBuilder.Configurations.Add(new SL_DepartmentMap());
            //modelBuilder.Configurations.Add(new SLDoctorMap());
            //modelBuilder.Configurations.Add(new DoctorClinicDepartmentMap());
        }

    }
}