using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobApplicationMap : EntityTypeConfiguration<JobApplication>
    {
        public JobApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplicationID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Contact)
                .HasMaxLength(20);

            this.Property(t => t.CV)
                .HasMaxLength(20);

            this.Property(t => t.IPAddress)
                .HasMaxLength(20);

            this.Property(t => t.IPCountry)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("JobApplication");
            this.Property(t => t.ApplicationID).HasColumnName("ApplicationID");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.CV).HasColumnName("CV");
            this.Property(t => t.Dated).HasColumnName("Dated");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
        }
    }
}
