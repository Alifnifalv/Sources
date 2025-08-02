using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eduegate.Domain.Entity.Models.Clinic;

namespace Eduegate.Domain.Entity.Models.Mapping.Clinic
{
    public class SLDoctorMap : EntityTypeConfiguration<SLDoctor>
    {
        public SLDoctorMap()
        {
            // Primary Key
            this.HasKey(t => t.SLDoctorID);

            // Properties
            this.Property(t => t.SLDoctorID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SLDoctor1)
                .HasMaxLength(500);

            this.Property(t => t.SLDoctorAr)
                .HasMaxLength(500);

            this.Property(t => t.SLTiming)
                .IsFixedLength()
                .HasMaxLength(1000);

            this.Property(t => t.SLContact)
                .HasMaxLength(50);

            this.Property(t => t.SLMainDepartmentName)
                .HasMaxLength(200);

            this.Property(t => t.SLSubDepartmentName)
                .HasMaxLength(200);

            this.Property(t => t.SLWebsite)
                .HasMaxLength(50);

            this.Property(t => t.SLFacebook)
                .HasMaxLength(50);

            this.Property(t => t.SLTwitter)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SLDoctor");
            this.Property(t => t.SLDoctorID).HasColumnName("SLDoctorID");
            this.Property(t => t.SLMainDepartment).HasColumnName("SLMainDepartment");
            this.Property(t => t.SLSubDepartment).HasColumnName("SLSubDepartment");
            this.Property(t => t.SLDoctor1).HasColumnName("SLDoctor");
            this.Property(t => t.SLDoctorAr).HasColumnName("SLDoctorAr");
            this.Property(t => t.SLTiming).HasColumnName("SLTiming");
            this.Property(t => t.SLContact).HasColumnName("SLContact");
            this.Property(t => t.SLMainDepartmentName).HasColumnName("SLMainDepartmentName");
            this.Property(t => t.SLSubDepartmentName).HasColumnName("SLSubDepartmentName");
            this.Property(t => t.SLThumb).HasColumnName("SLThumb");
            this.Property(t => t.SLDesc).HasColumnName("SLDesc");
            this.Property(t => t.FeatureDoctor).HasColumnName("FeatureDoctor");
            this.Property(t => t.SLVideos).HasColumnName("SLVideos");
            this.Property(t => t.SLWebsite).HasColumnName("SLWebsite");
            this.Property(t => t.SLFacebook).HasColumnName("SLFacebook");
            this.Property(t => t.SLTwitter).HasColumnName("SLTwitter");
            this.Property(t => t.SLFiles).HasColumnName("SLFiles");
            this.Property(t => t.SLPosition).HasColumnName("SLPosition");
            this.Property(t => t.SLDept).HasColumnName("SLDept");
            this.Property(t => t.SLQualification).HasColumnName("SLQualification");
            this.Property(t => t.SLCertificates).HasColumnName("SLCertificates");
            this.Property(t => t.SlClinic).HasColumnName("SlClinic");
            this.Property(t => t.SLPositionAr).HasColumnName("SLPositionAr");
            this.Property(t => t.SLCertificatesAr).HasColumnName("SLCertificatesAr");
            this.Property(t => t.SLDocord).HasColumnName("SLDocord");
            this.Property(t => t.SlCheck).HasColumnName("SlCheck");
        }
    }
}
