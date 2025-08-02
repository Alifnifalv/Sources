using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ColumnGroupCounterMap : EntityTypeConfiguration<ColumnGroupCounter>
    {
        public ColumnGroupCounterMap()
        {
            // Primary Key
            this.HasKey(t => t.ColumnGroupCounterID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ColumnGroupCounter");
            this.Property(t => t.ColumnGroupCounterID).HasColumnName("ColumnGroupCounterID");
            this.Property(t => t.RefColumnID).HasColumnName("RefColumnID");
            this.Property(t => t.ColumnGroupCount).HasColumnName("ColumnGroupCount");
        }
    }
}
