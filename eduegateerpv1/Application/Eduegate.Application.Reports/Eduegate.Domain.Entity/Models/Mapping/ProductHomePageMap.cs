using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductHomePageMap : EntityTypeConfiguration<ProductHomePage>
    {
        public ProductHomePageMap()
        {
            // Primary Key
            this.HasKey(t => t.RefHomePageProductID);

            // Properties
            this.Property(t => t.RefHomePageProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductHomePage");
            this.Property(t => t.RefHomePageProductID).HasColumnName("RefHomePageProductID");
            this.Property(t => t.Position).HasColumnName("Position");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithOptional(t => t.ProductHomePage);

        }
    }
}
