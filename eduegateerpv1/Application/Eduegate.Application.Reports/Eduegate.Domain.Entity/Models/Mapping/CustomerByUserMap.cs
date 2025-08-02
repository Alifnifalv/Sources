using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerByUserMap : EntityTypeConfiguration<CustomerByUser>
    {
        public CustomerByUserMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerByUserID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerByUser");
            this.Property(t => t.CustomerByUserID).HasColumnName("CustomerByUserID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
