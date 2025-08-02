using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WishListMap : EntityTypeConfiguration<WishList>
    {
        public WishListMap()
        {
            // Primary Key
            this.HasKey(t => t.WishListIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("WishList", "inventory");
            this.Property(t => t.WishListIID).HasColumnName("WishListIID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.SKUID).HasColumnName("SKUID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.IsWishList).HasColumnName("IsWishList");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.WishLists)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
