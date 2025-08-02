using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductCoutureNewArrivalMap : EntityTypeConfiguration<ProductCoutureNewArrival>
    {
        public ProductCoutureNewArrivalMap()
        {
            // Primary Key
            this.HasKey(t => t.RefNewArrivalProductID);

            // Properties
            this.Property(t => t.RefNewArrivalProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductCoutureNewArrivals");
            this.Property(t => t.RefNewArrivalProductID).HasColumnName("RefNewArrivalProductID");
            this.Property(t => t.Position).HasColumnName("Position");

            //Relationships
            this.HasRequired(t => t.ProductMaster)
                .WithOptional(t => t.ProductCoutureNewArrival);

        }
    }
}
