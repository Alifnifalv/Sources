using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherTypeMap : EntityTypeConfiguration<VoucherType>
    {
        public VoucherTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherTypeID);

            // Properties
            this.Property(t => t.VoucherTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VoucherTypes", "inventory");
            this.Property(t => t.VoucherTypeID).HasColumnName("VoucherTypeID");
            this.Property(t => t.VoucherTypeName).HasColumnName("VoucherTypeName");
            this.Property(t => t.IsMinimumAmtRequired).HasColumnName("IsMinimumAmtRequired");
        }
    }
}
