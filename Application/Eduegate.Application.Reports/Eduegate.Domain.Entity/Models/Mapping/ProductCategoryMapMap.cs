using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductCategoryMapMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.ProductCategoryMap>
    {
        public ProductCategoryMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductCategoryMapIID);

            // Properties
            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductCategoryMaps", "catalog");
            this.Property(t => t.ProductCategoryMapIID).HasColumnName("ProductCategoryMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.IsPrimary).HasColumnName("IsPrimary");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.ProductCategoryMaps)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductCategoryMaps)
                .HasForeignKey(d => d.ProductID);

        }
    }
}