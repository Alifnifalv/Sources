using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionStatusMap : EntityTypeConfiguration<TransactionStatus>
    {
        public TransactionStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionStatusID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransactionStatuses", "inventory");
            this.Property(t => t.TransactionStatusID).HasColumnName("TransactionStatusID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
