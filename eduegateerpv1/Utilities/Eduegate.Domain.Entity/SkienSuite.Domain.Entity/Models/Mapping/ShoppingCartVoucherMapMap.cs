using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartVoucherMapMap : EntityTypeConfiguration<ShoppingCartVoucherMap>
    {
        public ShoppingCartVoucherMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartVoucherMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ShoppingCartVoucherMaps", "inventory");
            this.Property(t => t.ShoppingCartVoucherMapIID).HasColumnName("ShoppingCartVoucherMapIID");
            this.Property(t => t.ShoppingCartID).HasColumnName("ShoppingCartID");
            this.Property(t => t.VoucherID).HasColumnName("VoucherID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.StatusID).HasColumnName("StatusID");

            // Relationships
            this.HasOptional(t => t.ShoppingCart)
                .WithMany(t => t.ShoppingCartVoucherMaps)
                .HasForeignKey(d => d.ShoppingCartID);
            this.HasOptional(t => t.Status)
                .WithMany(t => t.ShoppingCartVoucherMaps)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.Voucher)
                .WithMany(t => t.ShoppingCartVoucherMaps)
                .HasForeignKey(d => d.VoucherID);

        }
    }
}
