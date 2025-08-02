using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerAccountMapMap : EntityTypeConfiguration<CustomerAccountMap>
    {
        public CustomerAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerAccountMapIID);

            // Properties
            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerAccountMaps", "account");
            this.Property(t => t.CustomerAccountMapIID).HasColumnName("CustomerAccountMapIID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.CustomerAccountMaps)
                .HasForeignKey(d => d.AccountID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerAccountMaps)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.EntityTypeEntitlement)
                .WithMany(t => t.CustomerAccountMaps)
                .HasForeignKey(d => d.EntitlementID);

        }
    }
}
