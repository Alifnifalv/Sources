using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WalletCustomerLogMap : EntityTypeConfiguration<WalletCustomerLog>
    {
        public WalletCustomerLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogId);

            // Properties
            this.Property(t => t.Guid)
                .IsRequired()
                .HasMaxLength(40);
            //// Properties
            //this.Property(t => t.CustomerSessionId)
            //    .IsRequired()
            //    .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WalletCustomerLog", "wlt");
            this.Property(t => t.LogId).HasColumnName("LogId");
            this.Property(t => t.Guid).HasColumnName("Guid");
            this.Property(t => t.CustomerSessionId).HasColumnName("CustomerSessionId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CreatedDateTime).HasColumnName("CreatedDateTime");
        }
    }
}
