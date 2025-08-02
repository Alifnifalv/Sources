using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentMethodSiteMapMap : EntityTypeConfiguration<PaymentMethodSiteMap>
    {
        public PaymentMethodSiteMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SiteID, t.PaymentMethodID });

            // Properties
            this.Property(t => t.SiteID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PaymentMethodSiteMaps", "mutual");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.PaymentMethod)
                .WithMany(t => t.PaymentMethodSiteMaps)
                .HasForeignKey(d => d.PaymentMethodID);

        }
    }
}
