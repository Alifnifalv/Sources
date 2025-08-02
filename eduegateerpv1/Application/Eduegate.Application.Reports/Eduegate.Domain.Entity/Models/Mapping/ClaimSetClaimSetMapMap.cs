using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimSetClaimSetMapMap : EntityTypeConfiguration<ClaimSetClaimSetMap>
    {
        public ClaimSetClaimSetMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimSetClaimSetMapIID);

            // Properties
            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ClaimSetClaimSetMaps", "admin");
            this.Property(t => t.ClaimSetClaimSetMapIID).HasColumnName("ClaimSetClaimSetMapIID");
            this.Property(t => t.ClaimSetID).HasColumnName("ClaimSetID");
            this.Property(t => t.LinkedClaimSetID).HasColumnName("LinkedClaimSetID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ClaimSet)
                .WithMany(t => t.ClaimSetClaimSetMaps)
                .HasForeignKey(d => d.ClaimSetID);
            this.HasOptional(t => t.ClaimSet1)
                .WithMany(t => t.ClaimSetClaimSetMaps1)
                .HasForeignKey(d => d.LinkedClaimSetID);

        }
    }
}
