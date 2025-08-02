using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchGroupMap : EntityTypeConfiguration<BranchGroup>
    {
        public BranchGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchGroupIID);

            // Properties
            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BranchGroups", "mutual");
            this.Property(t => t.BranchGroupIID).HasColumnName("BranchGroupIID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.BranchGroupStatus)
                .WithMany(t => t.BranchGroups)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
