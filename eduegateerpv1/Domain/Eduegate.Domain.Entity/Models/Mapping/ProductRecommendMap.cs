using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductRecommendMap : EntityTypeConfiguration<ProductRecommend>
    {
        public ProductRecommendMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductRecommendID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductRecommend");
            this.Property(t => t.ProductRecommendID).HasColumnName("ProductRecommendID");
            this.Property(t => t.Ref1ProductID).HasColumnName("Ref1ProductID");
            this.Property(t => t.Ref2ProductID).HasColumnName("Ref2ProductID");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.ProductRecommends)
            //    .HasForeignKey(d => d.Ref1ProductID);
            //this.HasRequired(t => t.ProductMaster1)
            //    .WithMany(t => t.ProductRecommends1)
            //    .HasForeignKey(d => d.Ref2ProductID);

        }
    }
}
