using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchStatusMap : EntityTypeConfiguration<BranchStatus>
    {
        public BranchStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BranchStatuses", "mutual");
            this.Property(t => t.BranchStatusID).HasColumnName("BranchStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
