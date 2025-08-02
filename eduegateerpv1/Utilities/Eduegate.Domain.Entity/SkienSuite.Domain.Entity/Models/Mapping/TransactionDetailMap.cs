using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionDetailMap : EntityTypeConfiguration<TransactionDetail>
    {
        public TransactionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DetailIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Remark)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("TransactionDetails", "inventory");
            this.Property(t => t.DetailIID).HasColumnName("DetailIID");
            this.Property(t => t.HeadID).HasColumnName("HeadID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.WarrantyDate).HasColumnName("WarrantyDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.SerialNumber).HasColumnName("SerialNumber");
            this.Property(t => t.ParentDetailID).HasColumnName("ParentDetailID");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasOptional(t => t.TransactionDetail1)
                .WithMany(t => t.TransactionDetails1)
                .HasForeignKey(d => d.ParentDetailID);

        }
    }
}
