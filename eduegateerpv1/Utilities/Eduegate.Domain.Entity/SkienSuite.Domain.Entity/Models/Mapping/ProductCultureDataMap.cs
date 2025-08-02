using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductCultureDataMap : EntityTypeConfiguration<ProductCultureData>
    {
        public ProductCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.ProductID });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductName)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductDescription).HasColumnName("ProductDescription");
            this.Property(t => t.ProductDetails).HasColumnName("ProductDetails");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.ProductCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductCultureDatas)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
