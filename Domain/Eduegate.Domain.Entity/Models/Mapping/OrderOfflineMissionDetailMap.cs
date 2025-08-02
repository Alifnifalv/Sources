using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderOfflineMissionDetailMap : EntityTypeConfiguration<OrderOfflineMissionDetail>
    {
        public OrderOfflineMissionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderOfflineMissionDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderOfflineMissionDetails");
            this.Property(t => t.OrderOfflineMissionDetailID).HasColumnName("OrderOfflineMissionDetailID");
            this.Property(t => t.RefOrderOfflineMissionMasterID).HasColumnName("RefOrderOfflineMissionMasterID");
            this.Property(t => t.RefOrderOfflineID).HasColumnName("RefOrderOfflineID");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.UpdatedByTimeStamp).HasColumnName("UpdatedByTimeStamp");
            this.Property(t => t.UpdatedByID).HasColumnName("UpdatedByID");
        }
    }
}
