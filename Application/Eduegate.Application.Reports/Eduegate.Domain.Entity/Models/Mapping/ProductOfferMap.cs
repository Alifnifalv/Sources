using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductOfferMap : EntityTypeConfiguration<ProductOffer>
    {
        public ProductOfferMap()
        {
            // Primary Key
            this.HasKey(t => t.RefOfferProductID);

            // Properties
            this.Property(t => t.RefOfferProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductOffers");
            this.Property(t => t.RefOfferProductID).HasColumnName("RefOfferProductID");
            this.Property(t => t.Position).HasColumnName("Position");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithOptional(t => t.ProductOffer);

        }
    }
}
