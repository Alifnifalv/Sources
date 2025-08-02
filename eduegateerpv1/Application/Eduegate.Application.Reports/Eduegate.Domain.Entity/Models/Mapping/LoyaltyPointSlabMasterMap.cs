using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LoyaltyPointSlabMasterMap : EntityTypeConfiguration<LoyaltyPointSlabMaster>
    {
        public LoyaltyPointSlabMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.SlabID);

            // Properties
            // Table & Column Mappings
            this.ToTable("LoyaltyPointSlabMaster");
            this.Property(t => t.SlabID).HasColumnName("SlabID");
            this.Property(t => t.SlabPoint).HasColumnName("SlabPoint");
            this.Property(t => t.SlabAmount).HasColumnName("SlabAmount");
            this.Property(t => t.SlabValidity).HasColumnName("SlabValidity");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
