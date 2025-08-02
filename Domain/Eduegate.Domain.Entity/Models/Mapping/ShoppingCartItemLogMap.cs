using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartItemLogMap : EntityTypeConfiguration<ShoppingCartItemLog>
    {
        public ShoppingCartItemLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ShoppingCartItemLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.Dated).HasColumnName("Dated");
        }
    }
}
