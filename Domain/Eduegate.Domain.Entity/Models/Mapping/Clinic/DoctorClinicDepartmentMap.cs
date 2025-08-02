using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eduegate.Domain.Entity.Models.Clinic;

namespace Eduegate.Domain.Entity.Models.Mapping.Clinic
{
    public class DoctorClinicDepartmentMap : EntityTypeConfiguration<DoctorClinicDepartment>
    {
        public DoctorClinicDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DoctorClinicDepartment");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.SLDoctorID).HasColumnName("SLDoctorID");
            this.Property(t => t.SLClinicDepartment).HasColumnName("SLClinicDepartment");
        }
    }
}
