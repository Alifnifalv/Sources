using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderDeliveryDisplayHeadMapMap : EntityTypeConfiguration<OrderDeliveryDisplayHeadMap>
    {
        public OrderDeliveryDisplayHeadMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.HeadID, t.CultureID });

            // Properties
            this.Property(t => t.HeadID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryDisplayText)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("OrderDeliveryDisplayHeadMap", "distribution");
            this.Property(t => t.HeadID).HasColumnName("HeadID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.DeliveryDisplayText).HasColumnName("DeliveryDisplayText");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.OrderDeliveryDisplayHeadMaps)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.TransactionHead)
                .WithMany(t => t.OrderDeliveryDisplayHeadMaps)
                .HasForeignKey(d => d.HeadID);

        }
    }
}
