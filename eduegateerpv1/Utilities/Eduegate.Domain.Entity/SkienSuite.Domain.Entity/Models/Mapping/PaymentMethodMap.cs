using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentMethodMap : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentMethodID);

            // Properties
            this.Property(t => t.PaymentMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentMethod1)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.ImageName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PaymentMethods", "mutual");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.PaymentMethod1).HasColumnName("PaymentMethod");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
        }
    }
}
