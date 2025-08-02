using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class KuwaitNumbers2014Map : EntityTypeConfiguration<KuwaitNumbers2014>
    {
        public KuwaitNumbers2014Map()
        {
            // Primary Key
            this.HasKey(t => t.RangeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("KuwaitNumbers2014");
            this.Property(t => t.RangeID).HasColumnName("RangeID");
            this.Property(t => t.StartRange).HasColumnName("StartRange");
            this.Property(t => t.EndRange).HasColumnName("EndRange");
        }
    }
}
