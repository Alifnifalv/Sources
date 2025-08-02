using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductVoucherSettingMap : EntityTypeConfiguration<ProductVoucherSetting>
    {
        public ProductVoucherSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductVoucherSetting");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.VoucherAmount).HasColumnName("VoucherAmount");
            this.Property(t => t.VoucherValidity).HasColumnName("VoucherValidity");
        }
    }
}
