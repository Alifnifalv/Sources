using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherMap : EntityTypeConfiguration<Voucher>
    {
        public VoucherMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherIID);

            // Properties
            this.Property(t => t.VoucherNo)
                .HasMaxLength(200);

            this.Property(t => t.VoucherPin)
                .HasMaxLength(4);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Vouchers", "inventory");
            this.Property(t => t.VoucherIID).HasColumnName("VoucherIID");
            this.Property(t => t.VoucherNo).HasColumnName("VoucherNo");
            this.Property(t => t.VoucherPin).HasColumnName("VoucherPin");
            this.Property(t => t.VoucherTypeID).HasColumnName("VoucherTypeID");
            this.Property(t => t.IsSharable).HasColumnName("IsSharable");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.MinimumAmount).HasColumnName("MinimumAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.Vouchers)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.VoucherStatus)
                .WithMany(t => t.Vouchers)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.VoucherType)
                .WithMany(t => t.Vouchers)
                .HasForeignKey(d => d.VoucherTypeID);

        }
    }
}
