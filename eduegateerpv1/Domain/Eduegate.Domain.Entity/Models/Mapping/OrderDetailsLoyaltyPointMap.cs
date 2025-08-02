using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderDetailsLoyaltyPointMap : EntityTypeConfiguration<OrderDetailsLoyaltyPoint>
    {
        public OrderDetailsLoyaltyPointMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderDetailsLoyaltyPointsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderDetailsLoyaltyPoints");
            this.Property(t => t.OrderDetailsLoyaltyPointsID).HasColumnName("OrderDetailsLoyaltyPointsID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.ProductQuantity).HasColumnName("ProductQuantity");
            this.Property(t => t.Points).HasColumnName("Points");

            // Relationships
            this.HasRequired(t => t.OrderMaster)
                .WithMany(t => t.OrderDetailsLoyaltyPoints)
                .HasForeignKey(d => d.RefOrderID);
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.OrderDetailsLoyaltyPoints)
            //    .HasForeignKey(d => d.RefProductID);

        }
    }
}
