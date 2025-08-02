using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderLogMissionMap : EntityTypeConfiguration<OrderLogMission>
    {
        public OrderLogMissionMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderLogMissionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderLogMission");
            this.Property(t => t.OrderLogMissionID).HasColumnName("OrderLogMissionID");
            this.Property(t => t.RefLogOrderID).HasColumnName("RefLogOrderID");
            this.Property(t => t.OrderStatus).HasColumnName("OrderStatus");
            this.Property(t => t.RefDriverID).HasColumnName("RefDriverID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
