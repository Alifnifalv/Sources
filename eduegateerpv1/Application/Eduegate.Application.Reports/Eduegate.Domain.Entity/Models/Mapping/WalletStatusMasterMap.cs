using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WalletStatusMasterMap : EntityTypeConfiguration<WalletStatusMaster>
    {
        public WalletStatusMasterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.StatusId, t.Description });

            // Properties
            this.Property(t => t.StatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WalletStatusMaster", "wlt");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
