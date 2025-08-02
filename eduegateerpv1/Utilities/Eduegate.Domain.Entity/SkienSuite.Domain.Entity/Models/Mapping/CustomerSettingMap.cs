using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSettingMap : EntityTypeConfiguration<CustomerSetting>
    {
        public CustomerSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSettingIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerSettings", "mutual");
            this.Property(t => t.CustomerSettingIID).HasColumnName("CustomerSettingIID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CurrentLoyaltyPoints).HasColumnName("CurrentLoyaltyPoints");
            this.Property(t => t.TotalLoyaltyPoints).HasColumnName("TotalLoyaltyPoints");
            this.Property(t => t.IsVerified).HasColumnName("IsVerified");
            this.Property(t => t.IsConfirmed).HasColumnName("IsConfirmed");
            this.Property(t => t.IsBlocked).HasColumnName("IsBlocked");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.CustomerSettings)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
