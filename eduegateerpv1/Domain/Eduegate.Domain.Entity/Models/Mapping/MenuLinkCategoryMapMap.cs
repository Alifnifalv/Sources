using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MenuLinkCategoryMapMap : EntityTypeConfiguration<MenuLinkCategoryMap>
    {
        public MenuLinkCategoryMapMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuLinkCategoryMapIID);

            // Properties
            this.Property(t => t.ActionLink)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("MenuLinkCategoryMaps", "catalog");
            this.Property(t => t.MenuLinkCategoryMapIID).HasColumnName("MenuLinkCategoryMapIID");
            this.Property(t => t.MenuLinkID).HasColumnName("MenuLinkID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ActionLink).HasColumnName("ActionLink");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.MenuLinkCategoryMaps)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.MenuLink)
                .WithMany(t => t.MenuLinkCategoryMaps)
                .HasForeignKey(d => d.MenuLinkID);
            this.HasOptional(t => t.Site)
                .WithMany(t => t.MenuLinkCategoryMaps)
                .HasForeignKey(d => d.SiteID);

        }
    }
}
