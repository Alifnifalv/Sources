using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartSaveforLaterMap : EntityTypeConfiguration<ShoppingCartSaveforLater>
    {
        public ShoppingCartSaveforLaterMap()
        {
            // Primary Key
            this.HasKey(t => t.SaveforLaterID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ShoppingCartSaveforLater");
            this.Property(t => t.SaveforLaterID).HasColumnName("SaveforLaterID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
