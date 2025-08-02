using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class RouteMap : EntityTypeConfiguration<Route>
    {
        public RouteMap()
        {
            // Primary Key
            this.HasKey(t => t.RouteID);

            // Properties
            this.Property(t => t.RouteID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Routes", "distribution");
            this.Property(t => t.RouteID).HasColumnName("RouteID");
            this.Property(t => t.WarehouseID).HasColumnName("WarehouseID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");

            // Relationships
            this.HasOptional(t => t.Warehouse)
                .WithMany(t => t.Routes)
                .HasForeignKey(d => d.WarehouseID);
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Route)
                .HasForeignKey(d => d.CountryID);

        }
    }
}
