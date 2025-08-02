using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CitySearchViewMap : EntityTypeConfiguration<CitySearchView>
    {
        public CitySearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.CityID);

            // Properties
            this.Property(t => t.CityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CityName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CitySearchView", "mutual");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.CityName).HasColumnName("CityName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
