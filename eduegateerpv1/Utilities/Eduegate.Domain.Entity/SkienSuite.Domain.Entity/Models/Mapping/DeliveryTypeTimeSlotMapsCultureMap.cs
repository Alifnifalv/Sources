using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeTimeSlotMapsCultureMap : EntityTypeConfiguration<DeliveryTypeTimeSlotMapsCulture>
    {
        public DeliveryTypeTimeSlotMapsCultureMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.DeliveryTypeTimeSlotMapID });

            // Properties
            this.Property(t => t.DeliveryTypeTimeSlotMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CutOffDisplayText)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DeliveryTypeTimeSlotMapsCulture", "inventory");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.DeliveryTypeTimeSlotMapID).HasColumnName("DeliveryTypeTimeSlotMapID");
            this.Property(t => t.CutOffDisplayText).HasColumnName("CutOffDisplayText");

            // Relationships
            this.HasRequired(t => t.DeliveryTypeTimeSlotMap)
                .WithMany(t => t.DeliveryTypeTimeSlotMapsCultures)
                .HasForeignKey(d => d.DeliveryTypeTimeSlotMapID);
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.DeliveryTypeTimeSlotMapsCultures)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
