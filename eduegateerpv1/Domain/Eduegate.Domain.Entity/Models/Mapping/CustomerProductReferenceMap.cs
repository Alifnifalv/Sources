using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerProductReferenceMap : EntityTypeConfiguration<CustomerProductReference>
    {
        public CustomerProductReferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerProductReferenceIID);

            // Properties
            this.Property(t => t.BarCode)
                .HasMaxLength(50);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerProductReferences", "catalog");
            this.Property(t => t.CustomerProductReferenceIID).HasColumnName("CustomerProductReferenceIID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.CustomerProductReferences)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.CustomerProductReferences)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
