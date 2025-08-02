using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class GiftSlotProductMap : EntityTypeConfiguration<GiftSlotProduct>
    {
        public GiftSlotProductMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            // Table & Column Mappings
            this.ToTable("GiftSlotProduct");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.RefSlotID).HasColumnName("RefSlotID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
        }
    }
}
