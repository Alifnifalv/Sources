using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntitlementMapMap : EntityTypeConfiguration<EntitlementMap>
    {
        public EntitlementMapMap()
        {
            // Primary Key
            this.HasKey(t => t.EntitlementMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntitlementMaps", "mutual");
            this.Property(t => t.EntitlementMapIID).HasColumnName("EntitlementMapIID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.IsLocked).HasColumnName("IsLocked");
            this.Property(t => t.EntitlementAmount).HasColumnName("EntitlementAmount");
            this.Property(t => t.EntitlementDays).HasColumnName("EntitlementDays");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.EntityTypeEntitlement)
                .WithMany(t => t.EntitlementMaps)
                .HasForeignKey(d => d.EntitlementID);

        }
    }
}
