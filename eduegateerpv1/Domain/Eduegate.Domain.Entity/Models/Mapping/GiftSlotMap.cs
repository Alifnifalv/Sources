using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class GiftSlotMap : EntityTypeConfiguration<GiftSlot>
    {
        public GiftSlotMap()
        {
            // Primary Key
            this.HasKey(t => t.SlotID);

            // Properties
            this.Property(t => t.SlotName)
                .HasMaxLength(30);

            this.Property(t => t.SlotNameAr)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("GiftSlot");
            this.Property(t => t.SlotID).HasColumnName("SlotID");
            this.Property(t => t.SlotName).HasColumnName("SlotName");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.SlotNameAr).HasColumnName("SlotNameAr");
        }
    }
}
