using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductManagerMap : EntityTypeConfiguration<ProductManager>
    {
        public ProductManagerMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductManagerUserID);

            // Properties
            this.Property(t => t.ProductManagerUserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContactNo)
                .HasMaxLength(20);

            this.Property(t => t.SubOrdinateEmail)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProductManager");
            this.Property(t => t.ProductManagerUserID).HasColumnName("ProductManagerUserID");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.SubOrdinateEmail).HasColumnName("SubOrdinateEmail");
        }
    }
}
