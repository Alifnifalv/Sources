using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductTypeMap : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductTypeID);

            // Properties
            this.Property(t => t.ProductTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductTypes", "catalog");
            this.Property(t => t.ProductTypeID).HasColumnName("ProductTypeID");
            this.Property(t => t.ProductTypeName).HasColumnName("ProductTypeName");
        }
    }
}
