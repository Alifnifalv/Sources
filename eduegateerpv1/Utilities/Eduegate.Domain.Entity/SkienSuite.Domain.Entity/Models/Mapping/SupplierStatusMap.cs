using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierStatusMap : EntityTypeConfiguration<SupplierStatus>
    {
        public SupplierStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SupplierStatuses", "mutual");
            this.Property(t => t.SupplierStatusID).HasColumnName("SupplierStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
