using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceProviderSettingMap : EntityTypeConfiguration<ServiceProviderSetting>
    {
        public ServiceProviderSettingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ServiceProviderID, t.SettingCode });

            // Properties
            this.Property(t => t.ServiceProviderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SettingCode)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SettingValue)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ServiceProviderSettings", "distribution");
            this.Property(t => t.ServiceProviderID).HasColumnName("ServiceProviderID");
            this.Property(t => t.SettingCode).HasColumnName("SettingCode");
            this.Property(t => t.SettingValue).HasColumnName("SettingValue");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
