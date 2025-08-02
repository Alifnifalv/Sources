using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.LocationIID);

            // Properties
            this.Property(t => t.LocationCode)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(150);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Locations", "inventory");
            this.Property(t => t.LocationIID).HasColumnName("LocationIID");
            this.Property(t => t.LocationCode).HasColumnName("LocationCode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.LocationTypeID).HasColumnName("LocationTypeID");
            this.Property(t => t.Barcode).HasColumnName("Barcode");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.Locations)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.LocationType)
                .WithMany(t => t.Locations)
                .HasForeignKey(d => d.LocationTypeID);

        }
    }
}
