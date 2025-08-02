using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerGroupSearchViewMap : EntityTypeConfiguration<CustomerGroupSearchView>
    {
        public CustomerGroupSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerGroupIID);

            // Properties
            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CustomerGroupSearchView", "mutual");
            this.Property(t => t.CustomerGroupIID).HasColumnName("CustomerGroupIID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
        }
    }
}
