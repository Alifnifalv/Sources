using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMissionMasterMap : EntityTypeConfiguration<OrderMissionMaster>
    {
        public OrderMissionMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMissionMasterID);

            // Properties
            this.Property(t => t.MissionName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("OrderMissionMaster");
            this.Property(t => t.OrderMissionMasterID).HasColumnName("OrderMissionMasterID");
            this.Property(t => t.MissionName).HasColumnName("MissionName");
            this.Property(t => t.RefDriverMasterID).HasColumnName("RefDriverMasterID");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
