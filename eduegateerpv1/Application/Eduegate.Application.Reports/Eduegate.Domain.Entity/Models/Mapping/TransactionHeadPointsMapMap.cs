using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadPointsMapMap : EntityTypeConfiguration<TransactionHeadPointsMap>
    {
        public TransactionHeadPointsMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionHeadPointsMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransactionHeadPointsMap", "inventory");
            this.Property(t => t.TransactionHeadPointsMapIID).HasColumnName("TransactionHeadPointsMapIID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.LoyaltyPoints).HasColumnName("LoyaltyPoints");
            this.Property(t => t.CategorizationPoints).HasColumnName("CategorizationPoints");

            // Relationships
            this.HasRequired(t => t.TransactionHead)
                .WithMany(t => t.TransactionHeadPointsMaps)
                .HasForeignKey(d => d.TransactionHeadID);

        }
    }
}
