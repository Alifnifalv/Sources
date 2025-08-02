using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSlabMultipriceIntlMap : EntityTypeConfiguration<CustomerSlabMultipriceIntl>
    {
        public CustomerSlabMultipriceIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSlabMultipriceIntlID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerSlabMultipriceIntl");
            this.Property(t => t.CustomerSlabMultipriceIntlID).HasColumnName("CustomerSlabMultipriceIntlID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefSlabID).HasColumnName("RefSlabID");
            this.Property(t => t.DiscountPer).HasColumnName("DiscountPer");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.DiscountPerUnBranded).HasColumnName("DiscountPerUnBranded");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
        }
    }
}
