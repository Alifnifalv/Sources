using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eduegate.Domain.Entity.Models.Clinic;

namespace Eduegate.Domain.Entity.Models.Mapping.Clinic
{
    public class SL_DepartmentMap : EntityTypeConfiguration<SL_Department>
    {
        public SL_DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.SLDepartmentID);

            // Properties
            this.Property(t => t.SLDepartmentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SLDepartmentName)
                .HasMaxLength(200);

            this.Property(t => t.SLPage)
                .HasMaxLength(400);

            this.Property(t => t.SLDepartmentNameAr)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SL_Department");
            this.Property(t => t.SLDepartmentID).HasColumnName("SLDepartmentID");
            this.Property(t => t.SLDepartmentName).HasColumnName("SLDepartmentName");
            this.Property(t => t.SLParentCat).HasColumnName("SLParentCat");
            this.Property(t => t.SLPage).HasColumnName("SLPage");
            this.Property(t => t.SlClinic).HasColumnName("SlClinic");
            this.Property(t => t.SLThumb).HasColumnName("SLThumb");
            this.Property(t => t.SLThumbImage).HasColumnName("SLThumbImage");
            this.Property(t => t.SLDepartDescription).HasColumnName("SLDepartDescription");
            this.Property(t => t.SLDepartmentNameAr).HasColumnName("SLDepartmentNameAr");
            this.Property(t => t.SLDepartDescriptionAr).HasColumnName("SLDepartDescriptionAr");
            this.Property(t => t.SLDord).HasColumnName("SLDord");
        }
    }
}
