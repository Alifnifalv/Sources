using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NotifyMap : EntityTypeConfiguration<Notify>
    {
        public NotifyMap()
        {
            // Primary Key
            this.HasKey(t => t.NotifyIID);

            // Properties
            this.Property(t => t.EmailID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Notify", "inventory");
            this.Property(t => t.NotifyIID).HasColumnName("NotifyIID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.IsEmailSend).HasColumnName("IsEmailSend");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.Notifies)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.Site)
                .WithMany(t => t.Notifies)
                .HasForeignKey(d => d.SiteID);

        }
    }
}
