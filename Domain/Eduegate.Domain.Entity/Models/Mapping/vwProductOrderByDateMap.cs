using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductOrderByDateMap : EntityTypeConfiguration<vwProductOrderByDate>
    {
        public vwProductOrderByDateMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefOrderProductID, t.OrderDate, t.OrderQuantity, t.RefOrderID });

            // Properties
            this.Property(t => t.RefOrderProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrderQuantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefOrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductOrderByDate");
            this.Property(t => t.RefOrderProductID).HasColumnName("RefOrderProductID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.OrderQuantity).HasColumnName("OrderQuantity");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
        }
    }
}
