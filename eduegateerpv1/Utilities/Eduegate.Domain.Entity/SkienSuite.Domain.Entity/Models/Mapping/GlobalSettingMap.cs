using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class GlobalSettingMap : EntityTypeConfiguration<GlobalSetting>
    {
        public GlobalSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.GlobalSettingIID);

            // Properties
            this.Property(t => t.GlobalSettingKey)
                .HasMaxLength(300);

            this.Property(t => t.GlobalSettingValue)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("GlobalSettings", "setting");
            this.Property(t => t.GlobalSettingIID).HasColumnName("GlobalSettingIID");
            this.Property(t => t.GlobalSettingKey).HasColumnName("GlobalSettingKey");
            this.Property(t => t.GlobalSettingValue).HasColumnName("GlobalSettingValue");
        }
    }
}
