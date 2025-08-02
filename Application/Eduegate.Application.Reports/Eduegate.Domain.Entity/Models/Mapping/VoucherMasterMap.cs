using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherMasterMap : EntityTypeConfiguration<VoucherMaster>
    {
        public VoucherMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherID);

            // Properties
            this.Property(t => t.VoucherNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VoucherPin)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.VoucherType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("VoucherMaster");
            this.Property(t => t.VoucherID).HasColumnName("VoucherID");
            this.Property(t => t.VoucherNo).HasColumnName("VoucherNo");
            this.Property(t => t.VoucherPin).HasColumnName("VoucherPin");
            this.Property(t => t.VoucherType).HasColumnName("VoucherType");
            this.Property(t => t.IsSharable).HasColumnName("IsSharable");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.VoucherPoint).HasColumnName("VoucherPoint");
            this.Property(t => t.VoucherAmount).HasColumnName("VoucherAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.Validity).HasColumnName("Validity");
            this.Property(t => t.ValidTillDate).HasColumnName("ValidTillDate");
            this.Property(t => t.GenerateDate).HasColumnName("GenerateDate");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.IsRedeemed).HasColumnName("IsRedeemed");
            this.Property(t => t.RefOrderItemID).HasColumnName("RefOrderItemID");
            this.Property(t => t.VoucherDiscount).HasColumnName("VoucherDiscount");
            this.Property(t => t.isRefund).HasColumnName("isRefund");
            this.Property(t => t.isRefundForOrder).HasColumnName("isRefundForOrder");
            this.Property(t => t.MinAmount).HasColumnName("MinAmount");
        }
    }
}
