using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeTimeSlotMapMap : EntityTypeConfiguration<DeliveryTypeTimeSlotMap>
    {
        public DeliveryTypeTimeSlotMapMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryTypeTimeSlotMapIID);

            // Properties
            this.Property(t => t.CutOffDisplayText)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DeliveryTypeTimeSlotMaps", "inventory");
            this.Property(t => t.DeliveryTypeTimeSlotMapIID).HasColumnName("DeliveryTypeTimeSlotMapIID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.TimeFrom).HasColumnName("TimeFrom");
            this.Property(t => t.TimeTo).HasColumnName("TimeTo");
            this.Property(t => t.IsCutOff).HasColumnName("IsCutOff");
            this.Property(t => t.CutOffDays).HasColumnName("CutOffDays");
            this.Property(t => t.CutOffTime).HasColumnName("CutOffTime");
            this.Property(t => t.CutOffHour).HasColumnName("CutOffHour");
            this.Property(t => t.CutOffDisplayText).HasColumnName("CutOffDisplayText");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.DeliveryTypeTimeSlotMaps)
                .HasForeignKey(d => d.DeliveryTypeID);

        }
    }
}
