using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class KuwaitNumberMap : EntityTypeConfiguration<KuwaitNumber>
    {
        public KuwaitNumberMap()
        {
            // Primary Key
            this.HasKey(t => t.RangeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("KuwaitNumbers");
            this.Property(t => t.RangeID).HasColumnName("RangeID");
            this.Property(t => t.StartRange).HasColumnName("StartRange");
            this.Property(t => t.EndRange).HasColumnName("EndRange");
        }
    }
}
