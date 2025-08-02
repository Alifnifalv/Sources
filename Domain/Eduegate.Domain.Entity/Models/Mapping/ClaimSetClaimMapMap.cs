using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimSetClaimMapMap : EntityTypeConfiguration<ClaimSetClaimMap>
    {
        public ClaimSetClaimMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimSetClaimMapIID);

            // Properties
            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ClaimSetClaimMaps", "admin");
            this.Property(t => t.ClaimSetClaimMapIID).HasColumnName("ClaimSetClaimMapIID");
            this.Property(t => t.ClaimSetID).HasColumnName("ClaimSetID");
            this.Property(t => t.ClaimID).HasColumnName("ClaimID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Claim)
                .WithMany(t => t.ClaimSetClaimMaps)
                .HasForeignKey(d => d.ClaimID);
            this.HasOptional(t => t.ClaimSet)
                .WithMany(t => t.ClaimSetClaimMaps)
                .HasForeignKey(d => d.ClaimSetID);

        }
    }
}
