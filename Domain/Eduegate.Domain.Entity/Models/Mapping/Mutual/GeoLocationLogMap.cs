using Eduegate.Domain.Entity.Models.Mutual;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping.Mutual
{
    public class GeoLocationLogMap : EntityTypeConfiguration<GeoLocationLog>
    {
        public GeoLocationLogMap()
        {
            // Primary Key
            this.HasKey(t => t.GeoLocationLogIID);

            // Properties
            this.Property(t => t.Latitude)
                .HasMaxLength(50);
            this.Property(t => t.Longitude)
              .HasMaxLength(50);
            this.Property(t => t.ReferenceID1)
              .HasMaxLength(50);
            this.Property(t => t.ReferenceID2)
             .HasMaxLength(50);
            this.Property(t => t.ReferenceID3)
             .HasMaxLength(50);
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Table & Column Mappings
            this.ToTable("GeoLocationLogs", "mutual");
        }
    }
}
