using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MenuLinkCultureDataMap : EntityTypeConfiguration<MenuLinkCultureData>
    {
        public MenuLinkCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.MenuLinkID });

            // Properties
            this.Property(t => t.MenuLinkID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenuName)
                .HasMaxLength(100);

            this.Property(t => t.MenuTitle)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("MenuLinkCultureDatas", "setting");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.MenuLinkID).HasColumnName("MenuLinkID");
            this.Property(t => t.MenuName).HasColumnName("MenuName");
            this.Property(t => t.MenuTitle).HasColumnName("MenuTitle");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.MenuLinkCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.MenuLink)
                .WithMany(t => t.MenuLinkCultureDatas)
                .HasForeignKey(d => d.MenuLinkID);

        }
    }
}
