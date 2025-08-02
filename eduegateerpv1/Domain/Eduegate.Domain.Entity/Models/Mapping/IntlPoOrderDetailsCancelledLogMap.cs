using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoOrderDetailsCancelledLogMap : EntityTypeConfiguration<IntlPoOrderDetailsCancelledLog>
    {
        public IntlPoOrderDetailsCancelledLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoOrderDetailsCancelledLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("IntlPoOrderDetailsCancelledLog");
            this.Property(t => t.IntlPoOrderDetailsCancelledLogID).HasColumnName("IntlPoOrderDetailsCancelledLogID");
            this.Property(t => t.RefIntlPoOrderDetailsID).HasColumnName("RefIntlPoOrderDetailsID");
            this.Property(t => t.RefIntlPoBankAccountID).HasColumnName("RefIntlPoBankAccountID");
            this.Property(t => t.QtyCancelled).HasColumnName("QtyCancelled");
            this.Property(t => t.CancelledTotalUSD).HasColumnName("CancelledTotalUSD");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.IntlPoBankAccount)
                .WithMany(t => t.IntlPoOrderDetailsCancelledLogs)
                .HasForeignKey(d => d.RefIntlPoBankAccountID);
            this.HasRequired(t => t.IntlPoOrderDetail)
                .WithMany(t => t.IntlPoOrderDetailsCancelledLogs)
                .HasForeignKey(d => d.RefIntlPoOrderDetailsID);

        }
    }
}
