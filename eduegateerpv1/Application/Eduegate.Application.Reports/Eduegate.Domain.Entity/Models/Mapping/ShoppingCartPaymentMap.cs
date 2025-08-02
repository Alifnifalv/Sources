using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartPaymentMap : EntityTypeConfiguration<ShoppingCartPayment>
    {
        public ShoppingCartPaymentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CreatedOn, t.CustomerSessionID, t.Status });

            // Properties
            this.Property(t => t.CustomerSessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IP)
                .HasMaxLength(50);

            this.Property(t => t.Country)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShoppingCartPayment");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.TotalAmt).HasColumnName("TotalAmt");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.Country).HasColumnName("Country");
        }
    }
}
