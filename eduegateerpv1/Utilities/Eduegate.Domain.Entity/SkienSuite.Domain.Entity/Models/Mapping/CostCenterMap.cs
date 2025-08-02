using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CostCenterMap : EntityTypeConfiguration<CostCenter>
    {
        public CostCenterMap()
        {
            // Primary Key
            this.HasKey(t => t.CostCenterID);

            // Properties
            this.Property(t => t.CostCenterID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CostCenterName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CostCenters", "account");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.CostCenterName).HasColumnName("CostCenterName");
        }
    }
}
