using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceEmployeeMapMap : EntityTypeConfiguration<ServiceEmployeeMap>
    {
        public ServiceEmployeeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceEmployeeMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ServiceEmployeeMaps", "saloon");
            this.Property(t => t.ServiceEmployeeMapIID).HasColumnName("ServiceEmployeeMapIID");
            this.Property(t => t.ServiceID).HasColumnName("ServiceID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");

            // Relationships
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.ServiceEmployeeMaps)
                .HasForeignKey(d => d.EmployeeID);
            this.HasOptional(t => t.Service)
                .WithMany(t => t.ServiceEmployeeMaps)
                .HasForeignKey(d => d.ServiceID);

        }
    }
}
