using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.SettingCode });

            // Properties
            this.Property(t => t.SettingCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SettingValue)
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Settings", "setting");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.SettingCode).HasColumnName("SettingCode");
            this.Property(t => t.SettingValue).HasColumnName("SettingValue");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.LookupTypeID).HasColumnName("LookupTypeID");
            this.Property(t => t.ValueType).HasColumnName("ValueType");
        }
    }
}
