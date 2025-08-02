using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwVoucherTransactionMap : EntityTypeConfiguration<vwVoucherTransaction>
    {
        public vwVoucherTransactionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.VoucherNo, t.RefOrderID, t.Amount, t.VoucherType });

            // Properties
            this.Property(t => t.VoucherNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RefOrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Amount)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VoucherType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("vwVoucherTransaction");
            this.Property(t => t.VoucherNo).HasColumnName("VoucherNo");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.VoucherType).HasColumnName("VoucherType");
            this.Property(t => t.VoucherDiscount).HasColumnName("VoucherDiscount");
        }
    }
}
