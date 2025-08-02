using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMasterCODMap : EntityTypeConfiguration<OrderMasterCOD>
    {
        public OrderMasterCODMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMasterCodID);

            // Properties
            this.Property(t => t.CollectionRvNo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("OrderMasterCOD");
            this.Property(t => t.OrderMasterCodID).HasColumnName("OrderMasterCodID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.CollectionRvNo).HasColumnName("CollectionRvNo");
            this.Property(t => t.CollectionAmount).HasColumnName("CollectionAmount");
            this.Property(t => t.CollectionUserID).HasColumnName("CollectionUserID");
            this.Property(t => t.CollectionDateTime).HasColumnName("CollectionDateTime");
            this.Property(t => t.ReceivedAmount).HasColumnName("ReceivedAmount");
            this.Property(t => t.ReceiverUserID).HasColumnName("ReceiverUserID");
            this.Property(t => t.ReceivedDateTime).HasColumnName("ReceivedDateTime");
        }
    }
}
