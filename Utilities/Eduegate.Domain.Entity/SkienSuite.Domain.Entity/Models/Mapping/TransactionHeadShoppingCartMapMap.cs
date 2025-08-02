using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadShoppingCartMapMap : EntityTypeConfiguration<TransactionHeadShoppingCartMap>
    {
        public TransactionHeadShoppingCartMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionHeadShoppingCartMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TransactionHeadShoppingCartMaps", "inventory");
            this.Property(t => t.TransactionHeadShoppingCartMapIID).HasColumnName("TransactionHeadShoppingCartMapIID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.ShoppingCartID).HasColumnName("ShoppingCartID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.DeliveryCharge).HasColumnName("DeliveryCharge");

            // Relationships
            this.HasOptional(t => t.ShoppingCart)
                .WithMany(t => t.TransactionHeadShoppingCartMaps)
                .HasForeignKey(d => d.ShoppingCartID);
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.TransactionHeadShoppingCartMaps)
                .HasForeignKey(d => d.TransactionHeadID);
            this.HasOptional(t => t.Status)
                .WithMany(t => t.TransactionHeadShoppingCartMaps)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
