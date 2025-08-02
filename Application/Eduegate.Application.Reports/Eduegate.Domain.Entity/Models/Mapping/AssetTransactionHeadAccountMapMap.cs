using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AssetTransactionHeadAccountMapMap : EntityTypeConfiguration<AssetTransactionHeadAccountMap>
    {
        public AssetTransactionHeadAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.AssetTransactionHeadAccountMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AssetTransactionHeadAccountMaps", "asset");
            this.Property(t => t.AssetTransactionHeadAccountMapIID).HasColumnName("AssetTransactionHeadAccountMapIID");
            this.Property(t => t.AccountTransactionID).HasColumnName("AccountTransactionID");
            this.Property(t => t.AssetTransactionHeadID).HasColumnName("AssetTransactionHeadID");

            // Relationships
            this.HasRequired(t => t.AccountTransaction)
                .WithMany(t => t.AssetTransactionHeadAccountMaps)
                .HasForeignKey(d => d.AccountTransactionID);
            this.HasRequired(t => t.AssetTransactionHead)
                .WithMany(t => t.AssetTransactionHeadAccountMaps)
                .HasForeignKey(d => d.AssetTransactionHeadID);

        }
    }
}
