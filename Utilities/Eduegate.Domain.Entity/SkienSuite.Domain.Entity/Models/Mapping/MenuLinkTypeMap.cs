using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MenuLinkTypeMap : EntityTypeConfiguration<MenuLinkType>
    {
        public MenuLinkTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuLinkTypeID);

            // Properties
            this.Property(t => t.MenuLinkTypeName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("MenuLinkTypes", "setting");
            this.Property(t => t.MenuLinkTypeID).HasColumnName("MenuLinkTypeID");
            this.Property(t => t.MenuLinkTypeName).HasColumnName("MenuLinkTypeName");
        }
    }
}
