using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ZoneMap : EntityTypeConfiguration<Zone>
    {
        public ZoneMap()
        {
            // Primary Key
            this.HasKey(t => t.ZoneID);

            // Properties
            this.Property(t => t.ZoneID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ZoneName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Zones", "mutual");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.ZoneName).HasColumnName("ZoneName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Zones)
                .HasForeignKey(d => d.CountryID);

        }
    }
}
