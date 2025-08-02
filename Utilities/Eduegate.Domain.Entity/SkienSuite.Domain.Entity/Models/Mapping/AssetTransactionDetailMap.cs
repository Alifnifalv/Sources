using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AssetTransactionDetailMap : EntityTypeConfiguration<AssetTransactionDetail>
    {
        public AssetTransactionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DetailIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AssetTransactionDetails", "asset");
            this.Property(t => t.DetailIID).HasColumnName("DetailIID");
            this.Property(t => t.HeadID).HasColumnName("HeadID");
            this.Property(t => t.AssetID).HasColumnName("AssetID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.AccountID).HasColumnName("AccountID");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.AssetTransactionDetails)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.Asset)
                .WithMany(t => t.AssetTransactionDetails)
                .HasForeignKey(d => d.AssetID);
            this.HasOptional(t => t.AssetTransactionHead)
                .WithMany(t => t.AssetTransactionDetails)
                .HasForeignKey(d => d.HeadID);

        }
    }
}
