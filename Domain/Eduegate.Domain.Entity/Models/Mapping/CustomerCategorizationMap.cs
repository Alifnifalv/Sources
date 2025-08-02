using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerCategorizationMap : EntityTypeConfiguration<CustomerCategorization>
    {
        public CustomerCategorizationMap()
        {
            // Primary Key
            this.HasKey(t => t.SlabID);

            // Properties
            this.Property(t => t.CategoryName)
                .HasMaxLength(25);

            this.Property(t => t.CategoryNameAr)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("CustomerCategorization");
            this.Property(t => t.SlabID).HasColumnName("SlabID");
            this.Property(t => t.SlabPoint).HasColumnName("SlabPoint");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.ExpressDelivery).HasColumnName("ExpressDelivery");
            this.Property(t => t.NextDayDelivery).HasColumnName("NextDayDelivery");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.CategoryNameAr).HasColumnName("CategoryNameAr");
        }
    }
}
