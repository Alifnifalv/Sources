using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserJobApplicationMap : EntityTypeConfiguration<UserJobApplication>
    {
        public UserJobApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.JobApplicationIID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Telephone)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Resume)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserJobApplications", "cms");
            this.Property(t => t.JobApplicationIID).HasColumnName("JobApplicationIID");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Resume).HasColumnName("Resume");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
