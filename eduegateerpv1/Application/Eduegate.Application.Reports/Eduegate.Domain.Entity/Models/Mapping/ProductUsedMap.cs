using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductUsedMap : EntityTypeConfiguration<ProductUsed>
    {
        public ProductUsedMap()
        {
            // Primary Key
            this.HasKey(t => t.RefUsedProductID);

            // Properties
            this.Property(t => t.RefUsedProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductUsed");
            this.Property(t => t.RefUsedProductID).HasColumnName("RefUsedProductID");
            this.Property(t => t.Position).HasColumnName("Position");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
                //.WithOptional(t => t.ProductUsed);

        }
    }
}
