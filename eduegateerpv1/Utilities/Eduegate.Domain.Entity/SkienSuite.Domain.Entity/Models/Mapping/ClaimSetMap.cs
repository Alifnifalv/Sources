using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimSetMap : EntityTypeConfiguration<ClaimSet>
    {
        public ClaimSetMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimSetIID);

            // Properties
            this.Property(t => t.ClaimSetName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ClaimSets", "admin");
            this.Property(t => t.ClaimSetIID).HasColumnName("ClaimSetIID");
            this.Property(t => t.ClaimSetName).HasColumnName("ClaimSetName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
