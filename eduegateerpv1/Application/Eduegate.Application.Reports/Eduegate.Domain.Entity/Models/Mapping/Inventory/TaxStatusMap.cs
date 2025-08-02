using Eduegate.Domain.Entity.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Inventory
{
    public class TaxStatusMap : EntityTypeConfiguration<TaxStatus>
    {
        public TaxStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.TaxStatusID);

            // Properties
            this.Property(t => t.TaxStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TaxStatuses", "inventory");
            this.Property(t => t.TaxStatusID).HasColumnName("TaxStatusID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
