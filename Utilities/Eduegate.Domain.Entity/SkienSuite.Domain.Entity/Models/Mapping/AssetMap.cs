using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AssetMap : EntityTypeConfiguration<Asset>
    {
        public AssetMap()
        {
            // Primary Key
            this.HasKey(t => t.AssetIID);

            // Properties
            this.Property(t => t.AssetCode)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Assets", "asset");
            this.Property(t => t.AssetIID).HasColumnName("AssetIID");
            this.Property(t => t.AssetCategoryID).HasColumnName("AssetCategoryID");
            this.Property(t => t.AssetCode).HasColumnName("AssetCode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.AssetGlAccID).HasColumnName("AssetGlAccID");
            this.Property(t => t.AccumulatedDepGLAccID).HasColumnName("AccumulatedDepGLAccID");
            this.Property(t => t.DepreciationExpGLAccId).HasColumnName("DepreciationExpGLAccId");
            this.Property(t => t.DepreciationYears).HasColumnName("DepreciationYears");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.Assets)
                .HasForeignKey(d => d.AssetGlAccID);
            this.HasOptional(t => t.Account1)
                .WithMany(t => t.Assets1)
                .HasForeignKey(d => d.AccumulatedDepGLAccID);
            this.HasOptional(t => t.Account2)
                .WithMany(t => t.Assets2)
                .HasForeignKey(d => d.DepreciationExpGLAccId);
            this.HasOptional(t => t.AssetCategory)
                .WithMany(t => t.Assets)
                .HasForeignKey(d => d.AssetCategoryID);

        }
    }
}
