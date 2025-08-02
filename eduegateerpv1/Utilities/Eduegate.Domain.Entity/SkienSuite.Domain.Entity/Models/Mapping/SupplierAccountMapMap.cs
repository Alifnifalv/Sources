using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierAccountMapMap : EntityTypeConfiguration<SupplierAccountMap>
    {
        public SupplierAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierAccountMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SupplierAccountMaps", "account");
            this.Property(t => t.SupplierAccountMapIID).HasColumnName("SupplierAccountMapIID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.SupplierAccountMaps)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.EntityTypeEntitlement)
                .WithMany(t => t.SupplierAccountMaps)
                .HasForeignKey(d => d.EntitlementID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.SupplierAccountMaps)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
