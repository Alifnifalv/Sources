using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerLogVerificationMap : EntityTypeConfiguration<CustomerLogVerification>
    {
        public CustomerLogVerificationMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerLogVerification");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ChangedTo).HasColumnName("ChangedTo");
            this.Property(t => t.ChangedOn).HasColumnName("ChangedOn");
        }
    }
}
