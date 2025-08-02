using System.ComponentModel.DataAnnotations.Schema;
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
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage").HasPrecision(18, 3);
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice").HasPrecision(18, 3);
            this.Property(t => t.Amount).HasColumnName("Amount").HasPrecision(18, 3);
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate").HasPrecision(18, 6);
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
            this.Property(t => t.TaxAmount1).HasColumnName("TaxAmount1").HasPrecision(18, 3);
            this.Property(t => t.TaxAmount2).HasColumnName("TaxAmount2").HasPrecision(18, 3);
            this.Property(t => t.TaxTemplateID).HasColumnName("TaxTemplateID");
            this.Property(t => t.TaxPercentage).HasColumnName("TaxPercentage");
            this.Property(t => t.HasTaxInclusive).HasColumnName("HasTaxInclusive");
            this.Property(t => t.InclusiveTaxAmount).HasColumnName("InclusiveTaxAmount").HasPrecision(18, 3);
            this.Property(t => t.ExclusiveTaxAmount).HasColumnName("ExclusiveTaxAmount").HasPrecision(18, 3);
            this.Property(t => t.WarrantyStartDate).HasColumnName("WarrantyStartDate");
            this.Property(t => t.WarrantyEndDate).HasColumnName("WarrantyEndDate");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.ForeignAmount).HasColumnName("ForeignAmount").HasPrecision(18, 3);
            this.Property(t => t.ForeignRate).HasColumnName("ForeignRate").HasPrecision(18, 3);
            this.Property(t => t.LandingCost).HasColumnName("LandingCost").HasPrecision(18, 3);
            this.Property(t => t.LastCostPrice).HasColumnName("LastCostPrice").HasPrecision(18, 3);

            // Relationships
            //this.HasOptional(t => t.Product)
            //    //.WithMany(t => t.TransactionDetails)
            //    //.HasForeignKey(d => d.ProductID);
            //this.HasOptional(t => t.ProductSKUMap)
            //    .WithMany(t => t.TransactionDetails)
            //    .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.UnitID);

            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.HeadID);

            //this.HasOptional(t => t.TransactionDetail1)
            //     .WithMany(t => t.TransactionDetail1)
            //     .HasForeignKey(d => d.ParentDetailID);

        }
    }
}
