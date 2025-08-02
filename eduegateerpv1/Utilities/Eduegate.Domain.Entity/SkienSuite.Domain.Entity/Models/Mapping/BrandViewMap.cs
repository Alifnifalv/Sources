using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandViewMap : EntityTypeConfiguration<BrandView>
    {
        public BrandViewMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandIID);

            // Properties
            this.Property(t => t.BrandIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            this.Property(t => t.LogoFile)
                .HasMaxLength(300);

            this.Property(t => t.StatusName)
                .HasMaxLength(100);

            this.Property(t => t.Descirption)
                .HasMaxLength(1000);

            this.Property(t => t.RowCategory)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("BrandView", "cms");
            this.Property(t => t.BrandIID).HasColumnName("BrandIID");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.LogoFile).HasColumnName("LogoFile");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
            this.Property(t => t.Descirption).HasColumnName("Descirption");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
        }
    }
}
