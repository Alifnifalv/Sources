using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCustomerOrderMiniMarketMap : EntityTypeConfiguration<vwCustomerOrderMiniMarket>
    {
        public vwCustomerOrderMiniMarketMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderID, t.OrderDate, t.RefCustomerID, t.CustomerFirstName, t.CustomerLastName, t.EmailID });

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerFirstName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CustomerLastName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("vwCustomerOrderMiniMarket");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.CustomerFirstName).HasColumnName("CustomerFirstName");
            this.Property(t => t.CustomerLastName).HasColumnName("CustomerLastName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.MiniMarketTotal).HasColumnName("MiniMarketTotal");
        }
    }
}
