using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceProviderLogMap : EntityTypeConfiguration<ServiceProviderLog>
    {
        public ServiceProviderLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceProviderLogIID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ServiceProviderLogs", "distribution");
            this.Property(t => t.ServiceProviderLogIID).HasColumnName("ServiceProviderLogIID");
            this.Property(t => t.ServiceProviderID).HasColumnName("ServiceProviderID");
            this.Property(t => t.LogDateTime).HasColumnName("LogDateTime");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
