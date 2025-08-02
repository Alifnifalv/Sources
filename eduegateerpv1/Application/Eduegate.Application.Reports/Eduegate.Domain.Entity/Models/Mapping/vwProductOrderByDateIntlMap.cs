using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductOrderByDateIntlMap : EntityTypeConfiguration<vwProductOrderByDateIntl>
    {
        public vwProductOrderByDateIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductOrderByDateIntlID);

            // Properties
            // Table & Column Mappings
            this.ToTable("vwProductOrderByDateIntl");
            this.Property(t => t.ProductOrderByDateIntlID).HasColumnName("ProductOrderByDateIntlID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefOrderProductID).HasColumnName("RefOrderProductID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.OrderQuantity).HasColumnName("OrderQuantity");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
        }
    }
}
