using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryPriceListMapMap : EntityTypeConfiguration<CategoryPriceListMap>
    {
        public CategoryPriceListMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryPriceListItemMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CategoryPriceListMaps", "catalog");
            this.Property(t => t.CategoryPriceListItemMapIID).HasColumnName("CategoryPriceListItemMapIID");
            this.Property(t => t.PriceListID).HasColumnName("PriceListID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.UnitGroundID).HasColumnName("UnitGroundID");
            this.Property(t => t.CustomerGroupID).HasColumnName("CustomerGroupID");
            this.Property(t => t.SellingQuantityLimit).HasColumnName("SellingQuantityLimit");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.PricePercentage).HasColumnName("PricePercentage");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.CategoryPriceListMaps)
                .HasForeignKey(d => d.CategoryID);
            //this.HasOptional(t => t.UnitGroup)
            //    .WithMany(t => t.CategoryPriceListMaps)
            //    .HasForeignKey(d => d.UnitGroundID);
            this.HasOptional(t => t.CustomerGroup)
                .WithMany(t => t.CategoryPriceListMaps)
                .HasForeignKey(d => d.CustomerGroupID);
            //this.HasOptional(t => t.ProductPriceList)
            //    .WithMany(t => t.CategoryPriceListMaps)
            //    .HasForeignKey(d => d.PriceListID);

        }
    }
}
