using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductNewArrivalMap : EntityTypeConfiguration<ProductNewArrival>
    {
        public ProductNewArrivalMap()
        {
            // Primary Key
            this.HasKey(t => t.RefNewArrivalProductID);

            // Properties
            this.Property(t => t.RefNewArrivalProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductNewArrivals");
            this.Property(t => t.RefNewArrivalProductID).HasColumnName("RefNewArrivalProductID");
            this.Property(t => t.ProductStartDate).HasColumnName("ProductStartDate");
            this.Property(t => t.ProductEndDate).HasColumnName("ProductEndDate");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithOptional(t => t.ProductNewArrival);

        }
    }
}
