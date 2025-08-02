using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadPayablesMapMap : EntityTypeConfiguration<TransactionHeadPayablesMap>
    {
        public TransactionHeadPayablesMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionHeadPayablesMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransactionHeadPayablesMaps", "inventory");
            this.Property(t => t.TransactionHeadPayablesMapIID).HasColumnName("TransactionHeadPayablesMapIID");
            this.Property(t => t.PayableID).HasColumnName("PayableID");
            this.Property(t => t.HeadID).HasColumnName("HeadID");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.TransactionHeadPayablesMaps)
                .HasForeignKey(d => d.HeadID);

        }
    }
}
