using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MenuLinkMap : EntityTypeConfiguration<MenuLink>
    {
        public MenuLinkMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuLinkIID);

            // Properties
            this.Property(t => t.MenuName)
                .HasMaxLength(100);

            this.Property(t => t.ActionLink)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("MenuLinks", "setting");
            this.Property(t => t.MenuLinkIID).HasColumnName("MenuLinkIID");
            this.Property(t => t.ParentMenuID).HasColumnName("ParentMenuID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.MenuLinkTypeID).HasColumnName("MenuLinkTypeID");
            this.Property(t => t.ActionLink).HasColumnName("ActionLink");
            this.Property(t => t.ActionLink1).HasColumnName("ActionLink1");
            this.Property(t => t.ActionLink2).HasColumnName("ActionLink2");
            this.Property(t => t.ActionLink3).HasColumnName("ActionLink3");
            this.Property(t => t.Parameters).HasColumnName("Parameters");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.MenuTitle).HasColumnName("MenuTitle");
            this.Property(t => t.MenuIcon).HasColumnName("MenuIcon");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.MenuGroup).HasColumnName("MenuGroup");
        }
    }
}
