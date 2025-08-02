using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUCultureDataMap : EntityTypeConfiguration<ProductSKUCultureData>
    {
        public ProductSKUCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.ProductSKUMapID });

            // Properties
            this.Property(t => t.ProductSKUMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductSKUName)
                .HasMaxLength(1000);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductSKUCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.ProductSKUName).HasColumnName("ProductSKUName");
            this.Property(t => t.ProductSKUDescription).HasColumnName("ProductSKUDescription");
            this.Property(t => t.ProductSKUDetails).HasColumnName("ProductSKUDetails");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.ProductSKUCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.ProductSKUMap)
                .WithMany(t => t.ProductSKUCultureDatas)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
