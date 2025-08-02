using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMissionReturnExchangeMasterMap : EntityTypeConfiguration<OrderMissionReturnExchangeMaster>
    {
        public OrderMissionReturnExchangeMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMissonReturnExchangeMasterID);

            // Properties
            this.Property(t => t.MissionReturnExchangeName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ClosingRemark)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("OrderMissionReturnExchangeMaster");
            this.Property(t => t.OrderMissonReturnExchangeMasterID).HasColumnName("OrderMissonReturnExchangeMasterID");
            this.Property(t => t.MissionReturnExchangeName).HasColumnName("MissionReturnExchangeName");
            this.Property(t => t.RefDriverMasterID).HasColumnName("RefDriverMasterID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ClosingRemark).HasColumnName("ClosingRemark");
        }
    }
}
