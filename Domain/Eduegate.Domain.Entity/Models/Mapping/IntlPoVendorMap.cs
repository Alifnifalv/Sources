using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoVendorMap : EntityTypeConfiguration<IntlPoVendor>
    {
        public IntlPoVendorMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoVendorID);

            // Properties
            this.Property(t => t.IntlPoVendorCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.IntlPoVendorName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("IntlPoVendors");
            this.Property(t => t.IntlPoVendorID).HasColumnName("IntlPoVendorID");
            this.Property(t => t.IntlPoVendorCode).HasColumnName("IntlPoVendorCode");
            this.Property(t => t.IntlPoVendorName).HasColumnName("IntlPoVendorName");
            this.Property(t => t.IntlPoVendorActive).HasColumnName("IntlPoVendorActive");
        }
    }
}
