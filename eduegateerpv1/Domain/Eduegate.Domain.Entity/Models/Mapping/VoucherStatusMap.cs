using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherStatusMap : EntityTypeConfiguration<VoucherStatus>
    {
        public VoucherStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VoucherStatuses", "inventory");
            this.Property(t => t.VoucherStatusID).HasColumnName("VoucherStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
