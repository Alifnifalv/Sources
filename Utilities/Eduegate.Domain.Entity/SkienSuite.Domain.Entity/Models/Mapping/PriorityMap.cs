using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PriorityMap : EntityTypeConfiguration<Priority>
    {
        public PriorityMap()
        {
            // Primary Key
            this.HasKey(t => t.PriorityID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Priorities", "jobs");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
