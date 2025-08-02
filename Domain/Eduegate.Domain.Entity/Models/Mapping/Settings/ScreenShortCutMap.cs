using Eduegate.Domain.Entity.Models.Settings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Settings
{
    public class ScreenShortCutMap : EntityTypeConfiguration<ScreenShortCut>
    {
        public ScreenShortCutMap()
        {
            // Primary Key
            this.HasKey(t => t.ScreenShortCutID);

            // Properties
            this.Property(t => t.ScreenShortCutID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.KeyCode)
                .HasMaxLength(50);

            this.Property(t => t.ShortCutKey)
                .HasMaxLength(50);

            this.Property(t => t.Action)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ScreenShortCuts", "setting");
            this.Property(t => t.ScreenShortCutID).HasColumnName("ScreenShortCutID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.KeyCode).HasColumnName("KeyCode");
            this.Property(t => t.ShortCutKey).HasColumnName("ShortCutKey");
            this.Property(t => t.Action).HasColumnName("Action");
        }
    }
}
