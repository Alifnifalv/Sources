using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMissionDetailMap : EntityTypeConfiguration<OrderMissionDetail>
    {
        public OrderMissionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMissionDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderMissionDetails");
            this.Property(t => t.OrderMissionDetailID).HasColumnName("OrderMissionDetailID");
            this.Property(t => t.RefOrderMissionMasterID).HasColumnName("RefOrderMissionMasterID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.UpdatedByTimeStamp).HasColumnName("UpdatedByTimeStamp");
            this.Property(t => t.UpdatedByID).HasColumnName("UpdatedByID");
        }
    }
}
