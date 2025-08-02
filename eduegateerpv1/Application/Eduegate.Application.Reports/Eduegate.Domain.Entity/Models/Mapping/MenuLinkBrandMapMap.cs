using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MenuLinkBrandMapMap : EntityTypeConfiguration<MenuLinkBrandMap>
    {
        public MenuLinkBrandMapMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuLinkBrandMapIID);

            // Properties
            this.Property(t => t.ActionLink)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("MenuLinkBrandMaps", "cms");
            this.Property(t => t.MenuLinkBrandMapIID).HasColumnName("MenuLinkBrandMapIID");
            this.Property(t => t.MenuLinkID).HasColumnName("MenuLinkID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.ActionLink).HasColumnName("ActionLink");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.MenuLinkBrandMaps)
                .HasForeignKey(d => d.BrandID);
            this.HasOptional(t => t.MenuLink)
                .WithMany(t => t.MenuLinkBrandMaps)
                .HasForeignKey(d => d.MenuLinkID);

        }
    }
}
