using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadAccountMapMap : EntityTypeConfiguration<TransactionHeadAccountMap>
    {
        public TransactionHeadAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionHeadAccountMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TransactionHeadAccountMaps", "inventory");
            this.Property(t => t.TransactionHeadAccountMapIID).HasColumnName("TransactionHeadAccountMapIID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.AccountTransactionID).HasColumnName("AccountTransactionID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.TransactionHeadAccountMaps)
                .HasForeignKey(d => d.TransactionHeadID);

        }
    }
}
