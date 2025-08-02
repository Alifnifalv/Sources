using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherCustomerTranMap : EntityTypeConfiguration<VoucherCustomerTran>
    {
        public VoucherCustomerTranMap()
        {
            // Primary Key
            this.HasKey(t => t.TransID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .HasMaxLength(50);

            this.Property(t => t.VoucherNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VoucherCustomerTrans");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.VoucherNo).HasColumnName("VoucherNo");
            this.Property(t => t.Dated).HasColumnName("Dated");
        }
    }
}
