using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadReceivablesMapMap : EntityTypeConfiguration<TransactionHeadReceivablesMap>
    {
        public TransactionHeadReceivablesMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionHeadReceivablesMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransactionHeadReceivablesMaps", "inventory");
            this.Property(t => t.TransactionHeadReceivablesMapIID).HasColumnName("TransactionHeadReceivablesMapIID");
            this.Property(t => t.ReceivableID).HasColumnName("ReceivableID");
            this.Property(t => t.HeadID).HasColumnName("HeadID");

            // Relationships
            this.HasRequired(t => t.TransactionHead)
                .WithMany(t => t.TransactionHeadReceivablesMaps)
                .HasForeignKey(d => d.HeadID);

        }
    }
}
