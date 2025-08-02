using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SettingCode, t.CompanyID });

            // Properties
            this.Property(t => t.SettingCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SettingValue)
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Settings", "setting");
            this.Property(t => t.SettingCode).HasColumnName("SettingCode");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.SettingValue).HasColumnName("SettingValue");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ShowProductImageForPOS).HasColumnName("ShowProductImageForPOS");
            this.Property(t => t.GroupName).HasColumnName("GroupName");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Settings)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
