using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderTransactionMap : EntityTypeConfiguration<OrderTransaction>
    {
        public OrderTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionID);

            // Properties
            this.Property(t => t.TransactionType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OrderTransaction");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.RefTransactionOrderID).HasColumnName("RefTransactionOrderID");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.TransactionAmount).HasColumnName("TransactionAmount");

            // Relationships
            this.HasRequired(t => t.OrderMaster)
                .WithMany(t => t.OrderTransactions)
                .HasForeignKey(d => d.RefTransactionOrderID);

        }
    }
}
