using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerPinSearchMap : EntityTypeConfiguration<CustomerPinSearch>
    {
        public CustomerPinSearchMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Telephone, t.UserID, t.SearchOn });

            // Properties
            this.Property(t => t.Telephone)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.UserID)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CustomerPinSearch");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.SearchOn).HasColumnName("SearchOn");
        }
    }
}
