using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwOrderItemMap : EntityTypeConfiguration<vwOrderItem>
    {
        public vwOrderItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefOrderID, t.Quantity, t.cancelqty, t.retqty, t.ProductDiscountPrice, t.RefOrderProductID, t.OrderItemID });

            // Properties
            this.Property(t => t.RefOrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.cancelqty)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.retqty)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductDiscountPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefOrderProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrderItemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("vwOrderItems");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.cancelqty).HasColumnName("cancelqty");
            this.Property(t => t.retqty).HasColumnName("retqty");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.RefOrderProductID).HasColumnName("RefOrderProductID");
            this.Property(t => t.OrderItemID).HasColumnName("OrderItemID");
            this.Property(t => t.ReturnApproveQty).HasColumnName("ReturnApproveQty");
        }
    }
}
