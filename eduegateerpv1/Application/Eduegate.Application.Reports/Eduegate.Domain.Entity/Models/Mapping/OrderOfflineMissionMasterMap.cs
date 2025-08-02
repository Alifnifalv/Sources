using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderOfflineMissionMasterMap : EntityTypeConfiguration<OrderOfflineMissionMaster>
    {
        public OrderOfflineMissionMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderOfflineMissionMasterID);

            // Properties
            this.Property(t => t.OrderOfflineMissionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrderOfflineMissionMaster");
            this.Property(t => t.OrderOfflineMissionMasterID).HasColumnName("OrderOfflineMissionMasterID");
            this.Property(t => t.OrderOfflineMissionName).HasColumnName("OrderOfflineMissionName");
            this.Property(t => t.RefDriverMasterID).HasColumnName("RefDriverMasterID");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
