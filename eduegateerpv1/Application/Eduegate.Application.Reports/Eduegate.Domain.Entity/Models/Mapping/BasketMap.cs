using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BasketMap : EntityTypeConfiguration<Basket>
    {
        public BasketMap()
        {
            // Primary Key
            this.HasKey(t => t.BasketID);

            // Properties
            this.Property(t => t.BasketID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BasketCode)
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Baskets", "jobs");
            this.Property(t => t.BasketID).HasColumnName("BasketID");
            this.Property(t => t.BasketCode).HasColumnName("BasketCode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Barcode).HasColumnName("Barcode");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
