using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerPointsLogMap : EntityTypeConfiguration<CustomerPointsLog>
    {
        public CustomerPointsLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.UpdatedBy)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("CustomerPointsLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.PrevLoyaltyPoints).HasColumnName("PrevLoyaltyPoints");
            this.Property(t => t.PrevCategorizationPoints).HasColumnName("PrevCategorizationPoints");
            this.Property(t => t.NextLoyaltyPoints).HasColumnName("NextLoyaltyPoints");
            this.Property(t => t.NextCategorizationPoints).HasColumnName("NextCategorizationPoints");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
