using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryBlockMap : EntityTypeConfiguration<CategoryBlock>
    {
        public CategoryBlockMap()
        {
            // Primary Key
            this.HasKey(t => t.BlockID);

            // Properties
            this.Property(t => t.BlockTitle)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BlockLink)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BlockType)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.BlockTitleAr)
                .HasMaxLength(255);

            this.Property(t => t.BlockLinkAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("CategoryBlocks");
            this.Property(t => t.BlockID).HasColumnName("BlockID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.BlockTitle).HasColumnName("BlockTitle");
            this.Property(t => t.BlockLink).HasColumnName("BlockLink");
            this.Property(t => t.BlockType).HasColumnName("BlockType");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.BlockTitleAr).HasColumnName("BlockTitleAr");
            this.Property(t => t.BlockLinkAr).HasColumnName("BlockLinkAr");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
        }
    }
}
