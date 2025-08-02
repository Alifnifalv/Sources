using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.HR.Models.Mapping
{
    public class DB_ApplicationFormMap : EntityTypeConfiguration<DB_ApplicationForm>
    {
        public DB_ApplicationFormMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ContactNo)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.CountryOfResidence)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.YearsOfExperience)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.PositionAppliedFor)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.CV)
                .IsRequired()
                .HasMaxLength(2048);

            this.Property(t => t.Nationality)
                .HasMaxLength(256);

            this.Property(t => t.Gender)
                .HasMaxLength(50);

            this.Property(t => t.Qualification)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ApplicationForms", "hr");
            this.Property(t => t.ApplicationFormIID).HasColumnName("ApplicationFormIID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.CountryOfResidence).HasColumnName("CountryOfResidence");
            this.Property(t => t.YearsOfExperience).HasColumnName("YearsOfExperience");
            this.Property(t => t.PositionAppliedFor).HasColumnName("PositionAppliedFor");
            this.Property(t => t.CV).HasColumnName("CV");
            this.Property(t => t.Nationality).HasColumnName("Nationality");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Qualification).HasColumnName("Qualification");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
