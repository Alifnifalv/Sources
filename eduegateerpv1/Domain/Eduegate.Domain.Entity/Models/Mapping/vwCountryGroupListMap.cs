using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCountryGroupListMap : EntityTypeConfiguration<vwCountryGroupList>
    {
        public vwCountryGroupListMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CountryID, t.RefGroupID, t.CountryCode, t.CountryNameEn, t.CountryNameAr, t.Active, t.GroupID });

            // Properties
            this.Property(t => t.CountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CountryCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.CountryNameEn)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CountryNameAr)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.GroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GroupType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwCountryGroupList");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.RefGroupID).HasColumnName("RefGroupID");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.CountryNameEn).HasColumnName("CountryNameEn");
            this.Property(t => t.CountryNameAr).HasColumnName("CountryNameAr");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.GroupType).HasColumnName("GroupType");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.MinWeight).HasColumnName("MinWeight");
            this.Property(t => t.MinAmount).HasColumnName("MinAmount");
            this.Property(t => t.AfterMinWeight).HasColumnName("AfterMinWeight");
            this.Property(t => t.AfterMinAmount).HasColumnName("AfterMinAmount");
        }
    }
}
