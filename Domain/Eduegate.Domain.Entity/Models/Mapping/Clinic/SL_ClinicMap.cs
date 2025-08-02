using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eduegate.Domain.Entity.Models.Clinic;

namespace Eduegate.Domain.Entity.Models.Mapping.Clinic
{
    public class SL_ClinicMap : EntityTypeConfiguration<SL_Clinic>
    {
        public SL_ClinicMap()
        {
            // Primary Key
            this.HasKey(t => t.SLClinicID);

            // Properties
            this.Property(t => t.SLClinicID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SLClinicName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SL_Clinic");
            this.Property(t => t.SLClinicID).HasColumnName("SLClinicID");
            this.Property(t => t.SLClinicName).HasColumnName("SLClinicName");
            this.Property(t => t.SLClinicDescription).HasColumnName("SLClinicDescription");
            this.Property(t => t.SLClinicDescription2).HasColumnName("SLClinicDescription2");
        }
    }
}
