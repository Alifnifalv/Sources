using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchGroupStatusMap : EntityTypeConfiguration<BranchGroupStatus>
    {
        public BranchGroupStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchGroupStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BranchGroupStatuses", "mutual");
            this.Property(t => t.BranchGroupStatusID).HasColumnName("BranchGroupStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
