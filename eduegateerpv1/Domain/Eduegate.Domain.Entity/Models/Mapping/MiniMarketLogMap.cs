using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MiniMarketLogMap : EntityTypeConfiguration<MiniMarketLog>
    {
        public MiniMarketLogMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            this.Property(t => t.ActionType)
                .HasMaxLength(50);

            this.Property(t => t.Action)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MiniMarketLog");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ActionTime).HasColumnName("ActionTime");
        }
    }
}
