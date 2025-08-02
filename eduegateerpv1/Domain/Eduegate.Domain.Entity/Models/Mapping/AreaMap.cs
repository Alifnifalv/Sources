using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AreaMap : EntityTypeConfiguration<Area>
    {
        public AreaMap()
        {
            // Primary Key
            this.HasKey(t => t.AreaID);

            // Properties
            this.Property(t => t.AreaID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AreaName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Areas", "mutual");
            this.Property(t => t.AreaID).HasColumnName("AreaID");
            this.Property(t => t.AreaName).HasColumnName("AreaName");
            this.Property(t => t.RouteID).HasColumnName("RouteID");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Route)
                .WithMany(t => t.Areas)
                .HasForeignKey(d => d.RouteID);
            this.HasOptional(t => t.City)
                .WithMany(t => t.Areas)
                .HasForeignKey(d => d.CityID);
            this.HasOptional(t => t.Zone)
                .WithMany(t => t.Areas)
                .HasForeignKey(d => d.ZoneID);
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Area)
                .HasForeignKey(d => d.CountryID);
        }
    }
}
