using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WalletTransactionDetailMap : EntityTypeConfiguration<WalletTransactionDetail>
    {
        public WalletTransactionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.WalletTransactionId);

            // Properties
            this.Property(t => t.CustomerWalletTranRef)
                .HasMaxLength(35)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            this.Property(t => t.RefOrderId)
                .HasMaxLength(20);

            this.Property(t => t.PaymentMethod)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("WalletTransactionDetail", "wlt");
            this.Property(t => t.WalletTransactionId).HasColumnName("WalletTransactionId");
            this.Property(t => t.CustomerWalletTranRef).HasColumnName("CustomerWalletTranRef");
            this.Property(t => t.RefTransactionRelationId).HasColumnName("RefTransactionRelationId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.AdditionalDetails).HasColumnName("AdditionalDetails");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.TrackId).HasColumnName("TrackId");
            this.Property(t => t.RefOrderId).HasColumnName("RefOrderId");
            this.Property(t => t.CreatedDateTime).HasColumnName("CreatedDateTime");
            this.Property(t => t.ModifiedDateTime).HasColumnName("ModifiedDateTime");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");

            // Relationships
            this.HasRequired(t => t.CustomerMaster)
                .WithMany(t => t.WalletTransactionDetails)
                .HasForeignKey(d => d.CustomerId);

        }
    }
}
