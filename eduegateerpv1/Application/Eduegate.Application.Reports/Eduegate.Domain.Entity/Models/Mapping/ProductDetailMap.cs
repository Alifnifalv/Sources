using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDetailMap : EntityTypeConfiguration<ProductDetail>
    {
        public ProductDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDetailsID);

            // Properties
            this.Property(t => t.CategoryColumnValue)
                .HasMaxLength(255);

            this.Property(t => t.GroupName)
                .HasMaxLength(30);

            this.Property(t => t.CategoryColumnValueAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProductDetails");
            this.Property(t => t.ProductDetailsID).HasColumnName("ProductDetailsID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.RefCategoryColumnID).HasColumnName("RefCategoryColumnID");
            this.Property(t => t.CategoryColumnValue).HasColumnName("CategoryColumnValue");
            this.Property(t => t.CategoryColumnValueRange).HasColumnName("CategoryColumnValueRange");
            this.Property(t => t.OrderSeq).HasColumnName("OrderSeq");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.CategoryColumnValueAr).HasColumnName("CategoryColumnValueAr");
            this.Property(t => t.CategoryColumnValueRangeAr).HasColumnName("CategoryColumnValueRangeAr");

            // Relationships
            this.HasRequired(t => t.CategoryColumn)
                .WithMany(t => t.ProductDetails)
                .HasForeignKey(d => d.RefCategoryColumnID);
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.ProductDetails1)
            //    .HasForeignKey(d => d.RefProductID);

        }
    }
}
