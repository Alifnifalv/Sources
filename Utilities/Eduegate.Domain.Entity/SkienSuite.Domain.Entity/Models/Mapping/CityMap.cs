using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            // Primary Key
            this.HasKey(t => t.CityID);

            // Properties
            this.Property(t => t.CityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CityName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Cities", "mutual");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CityName).HasColumnName("CityName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Cities)
                .HasForeignKey(d => d.CountryID);

        }
    }
}
