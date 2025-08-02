using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwOrderMasterItemMap : EntityTypeConfiguration<vwOrderMasterItem>
    {
        public vwOrderMasterItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderItemID, t.RefOrderProductID, t.ProductPrice, t.Quantity, t.ItemAmount, t.ItemCanceled, t.OrderID, t.RefCustomerID, t.OrderDate, t.SessionID, t.ProductDiscountPrice });

            // Properties
            this.Property(t => t.OrderItemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefOrderProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ItemAmount)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProductDiscountPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwOrderMasterItem");
            this.Property(t => t.OrderItemID).HasColumnName("OrderItemID");
            this.Property(t => t.RefOrderProductID).HasColumnName("RefOrderProductID");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ItemAmount).HasColumnName("ItemAmount");
            this.Property(t => t.ItemCanceled).HasColumnName("ItemCanceled");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.ReturnApproveQty).HasColumnName("ReturnApproveQty");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
        }
    }
}
