using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerPageLogMap : EntityTypeConfiguration<CustomerPageLog>
    {
        public CustomerPageLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.PageName)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("CustomerPageLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.PageName).HasColumnName("PageName");
            this.Property(t => t.LogOn).HasColumnName("LogOn");
        }
    }
}
