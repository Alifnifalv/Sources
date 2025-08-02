using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadEntitlementMapMap : EntityTypeConfiguration<TransactionHeadEntitlementMap>
    {
        public TransactionHeadEntitlementMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionHeadEntitlementMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransactionHeadEntitlementMap", "inventory");
            this.Property(t => t.TransactionHeadEntitlementMapIID).HasColumnName("TransactionHeadEntitlementMapIID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.Amount).HasColumnName("Amount").HasPrecision(18, 3);

            // Relationships
            this.HasRequired(t => t.TransactionHead)
                .WithMany(t => t.TransactionHeadEntitlementMaps)
                .HasForeignKey(d => d.TransactionHeadID);
            this.HasRequired(t => t.EntityTypeEntitlement)
                .WithMany(t => t.TransactionHeadEntitlementMaps)
                .HasForeignKey(d => d.EntitlementID);

        }
    }
}
