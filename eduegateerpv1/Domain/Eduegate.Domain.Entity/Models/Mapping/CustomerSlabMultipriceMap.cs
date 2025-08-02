using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSlabMultipriceMap : EntityTypeConfiguration<CustomerSlabMultiprice>
    {
        public CustomerSlabMultipriceMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSlabMultipriceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerSlabMultiprice");
            this.Property(t => t.CustomerSlabMultipriceID).HasColumnName("CustomerSlabMultipriceID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefSlabID).HasColumnName("RefSlabID");
            this.Property(t => t.DiscountPer).HasColumnName("DiscountPer");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.DiscountPerUnBranded).HasColumnName("DiscountPerUnBranded");
        }
    }
}
