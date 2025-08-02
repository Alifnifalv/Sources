using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategorySettingMap : EntityTypeConfiguration<CategorySetting>
    {
        public CategorySettingMap()
        {
            // Primary Key
            this.HasKey(t => t.CategorySettingsID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            this.Property(t => t.SettingCode)
                .HasMaxLength(50);

            this.Property(t => t.SettingValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CategorySettings", "catalog");
            this.Property(t => t.CategorySettingsID).HasColumnName("CategorySettingsID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SettingCode).HasColumnName("SettingCode");
            this.Property(t => t.SettingValue).HasColumnName("SettingValue");
            this.Property(t => t.UIControlTypeID).HasColumnName("UIControlTypeID");
            this.Property(t => t.LookUpID).HasColumnName("LookUpID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.CategorySettings)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.UIControlType)
                .WithMany(t => t.CategorySettings)
                .HasForeignKey(d => d.UIControlTypeID);

        }
    }
}
