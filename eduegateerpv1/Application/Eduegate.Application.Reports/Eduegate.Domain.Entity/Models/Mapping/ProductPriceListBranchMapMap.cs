using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListBranchMapMap : EntityTypeConfiguration<ProductPriceListBranchMap>
    {
        public ProductPriceListBranchMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListBranchMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListBranchMaps", "catalog");
            this.Property(t => t.ProductPriceListBranchMapIID).HasColumnName("ProductPriceListBranchMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.ProductPriceListBranchMaps)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListBranchMaps)
                .HasForeignKey(d => d.ProductPriceListID);

        }
    }
}
