using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductItemMap : EntityTypeConfiguration<vwProductItem>
    {
        public vwProductItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderDate, t.RefOrderProductID, t.ProductName, t.OrderID });

            // Properties
            this.Property(t => t.RefOrderProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductItem");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.RefOrderProductID).HasColumnName("RefOrderProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Qty).HasColumnName("Qty");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
        }
    }
}
