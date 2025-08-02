using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentGroupMap : EntityTypeConfiguration<PaymentGroup>
    {
        public PaymentGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentGroupID);

            // Properties
            this.Property(t => t.PaymentGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PaymentGroups", "mutual");
            this.Property(t => t.PaymentGroupID).HasColumnName("PaymentGroupID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.IsCustomerBlocked).HasColumnName("IsCustomerBlocked");
            this.Property(t => t.IsLocal).HasColumnName("IsLocal");
            this.Property(t => t.IsDigitalCart).HasColumnName("IsDigitalCart");
            this.Property(t => t.CreatedData).HasColumnName("CreatedData");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasMany(t => t.PaymentMethods)
                .WithMany(t => t.PaymentGroups)
                .Map(m =>
                    {
                        m.ToTable("PaymentGroupPaymentTypeMaps", "mutual");
                        m.MapLeftKey("PaymentGroupID");
                        m.MapRightKey("PaymentMethodID");
                    });

            this.HasRequired(t => t.PaymentGroup1)
                .WithOptional(t => t.PaymentGroups1);

        }
    }
}
