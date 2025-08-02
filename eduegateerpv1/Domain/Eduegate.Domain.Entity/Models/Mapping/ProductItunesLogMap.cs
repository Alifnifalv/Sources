using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductItunesLogMap : EntityTypeConfiguration<ProductItunesLog>
    {
        public ProductItunesLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.RequestPage)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProductItunesLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.RequestPage).HasColumnName("RequestPage");
            this.Property(t => t.RequestOn).HasColumnName("RequestOn");
        }
    }
}
