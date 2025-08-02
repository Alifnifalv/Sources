using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerGroupMap : EntityTypeConfiguration<CustomerGroup>
    {
        public CustomerGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerGroupIID);

            // Properties
            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerGroups", "mutual");
            this.Property(t => t.CustomerGroupIID).HasColumnName("CustomerGroupIID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.PointLimit).HasColumnName("PointLimit");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
