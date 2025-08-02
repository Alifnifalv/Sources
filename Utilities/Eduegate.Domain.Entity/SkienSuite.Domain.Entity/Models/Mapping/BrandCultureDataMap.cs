using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandCultureDataMap : EntityTypeConfiguration<BrandCultureData>
    {
        public BrandCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.BrandID });

            // Properties
            this.Property(t => t.BrandID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            this.Property(t => t.Descirption)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BrandCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.Descirption).HasColumnName("Descirption");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Brand)
                .WithMany(t => t.BrandCultureDatas)
                .HasForeignKey(d => d.BrandID);
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.BrandCultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
