using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ConditionMap : EntityTypeConfiguration<Condition>
    {
        public ConditionMap()
        {
            // Primary Key
            this.HasKey(t => t.ConditionID);

            // Properties
            this.Property(t => t.ConditionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Conditions", "setting");
            this.Property(t => t.ConditionID).HasColumnName("ConditionID");
            this.Property(t => t.ConditionName).HasColumnName("ConditionName");
        }
    }
}
