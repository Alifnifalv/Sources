using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSessionMap : EntityTypeConfiguration<CustomerSession>
    {
        public CustomerSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSessionGUID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerSession");
            this.Property(t => t.CustomerSessionGUID).HasColumnName("CustomerSessionGUID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.LastAccessed).HasColumnName("LastAccessed");
            this.Property(t => t.IsExpired).HasColumnName("IsExpired");
        }
    }
}
