using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceGroupMap : EntityTypeConfiguration<ServiceGroup>
    {
        public ServiceGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceGroupIID);

            // Properties
            this.Property(t => t.GroupName)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ServiceGroups", "saloon");
            this.Property(t => t.ServiceGroupIID).HasColumnName("ServiceGroupIID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
