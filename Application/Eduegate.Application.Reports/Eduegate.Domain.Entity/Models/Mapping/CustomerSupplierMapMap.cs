using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSupplierMapMap : EntityTypeConfiguration<CustomerSupplierMap>
    {
        public CustomerSupplierMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSupplierMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerSupplierMaps", "mutual");
            this.Property(t => t.CustomerSupplierMapIID).HasColumnName("CustomerSupplierMapIID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.CustomerSupplierMaps)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.CustomerSupplierMaps)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
