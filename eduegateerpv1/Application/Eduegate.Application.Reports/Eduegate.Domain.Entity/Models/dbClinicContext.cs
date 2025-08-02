using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models.Clinic;
using Eduegate.Domain.Entity.Models.Mapping.Clinic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class dbClinicContext: DbContext
    {
        static dbClinicContext()
        {
            Database.SetInitializer<dbClinicContext>(null);
        }

        public dbClinicContext()
            : base("Name=dbBlinkContext")
        {
        }

        public DbSet<AxDocument> AxDocuments { get; set; }
        public DbSet<AxImage> AxImages { get; set; }
        public DbSet<SL_Clinic> SL_Clinic { get; set; }
        public DbSet<SL_Department> SL_Department { get; set; }
        public DbSet<SLDoctor> SLDoctors { get; set; }
        public DbSet<DoctorClinicDepartment> DoctorClinicDepartments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AxDocumentMap());
            modelBuilder.Configurations.Add(new AxImageMap());
            modelBuilder.Configurations.Add(new SL_ClinicMap());
            modelBuilder.Configurations.Add(new SL_DepartmentMap());
            modelBuilder.Configurations.Add(new SLDoctorMap());
            modelBuilder.Configurations.Add(new DoctorClinicDepartmentMap());
        }

    }
}
