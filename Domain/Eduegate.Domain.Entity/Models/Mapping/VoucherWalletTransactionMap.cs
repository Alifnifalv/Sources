using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherWalletTransactionMap : EntityTypeConfiguration<VoucherWalletTransaction>
    {
        public VoucherWalletTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransID);

            // Properties
            this.Property(t => t.VoucherNo)
                .IsRequired()
                .HasMaxLength(50);

            // Properties
            this.Property(t => t.VoucherNo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VoucherWalletTransaction", "wlt");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.VoucherNo).HasColumnName("VoucherNo");
            this.Property(t => t.WalletTransactionID).HasColumnName("WalletTransactionID");
            //this.Property(t => t.Amount).HasColumnName("Amount");
        }
        
           

    }
}


