using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerLogMap : EntityTypeConfiguration<CustomerLog>
    {
        public CustomerLogMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefCustomerID, t.ActionTime, t.ActionLog, t.IpAddress, t.IpCountry });

            // Properties
            this.Property(t => t.RefCustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActionLog)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.IpAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IpCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ActionKey)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CustomerLog");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.ActionTime).HasColumnName("ActionTime");
            this.Property(t => t.ActionLog).HasColumnName("ActionLog");
            this.Property(t => t.IpAddress).HasColumnName("IpAddress");
            this.Property(t => t.IpCountry).HasColumnName("IpCountry");
            this.Property(t => t.ActionKey).HasColumnName("ActionKey");

            // Relationships
            this.HasRequired(t => t.CustomerMaster)
                .WithMany(t => t.CustomerLogs)
                .HasForeignKey(d => d.RefCustomerID);

        }
    }
}
