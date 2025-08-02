using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryChargeMap : EntityTypeConfiguration<DeliveryCharge>
    {
        public DeliveryChargeMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryChargeIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DeliveryCharges", "inventory");
            this.Property(t => t.DeliveryChargeIID).HasColumnName("DeliveryChargeIID");
            this.Property(t => t.ServiceProviderID).HasColumnName("ServiceProviderID");
            this.Property(t => t.FromCountryID).HasColumnName("FromCountryID");
            this.Property(t => t.ToCountryID).HasColumnName("ToCountryID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.CartStartRange).HasColumnName("CartStartRange");
            this.Property(t => t.CartEndRange).HasColumnName("CartEndRange");
            this.Property(t => t.WeightStartRange).HasColumnName("WeightStartRange");
            this.Property(t => t.WeightEndRange).HasColumnName("WeightEndRange");
            this.Property(t => t.DeliveryCharge1).HasColumnName("DeliveryCharge");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CountryGroupID).HasColumnName("CountryGroupID");
            this.Property(t => t.WeightRangeDivisor).HasColumnName("WeightRangeDivisor");
            this.Property(t => t.WeightChargeDivisor).HasColumnName("WeightChargeDivisor");

            // Relationships
            this.HasOptional(t => t.ServiceProviderCountryGroup)
                .WithMany(t => t.DeliveryCharges)
                .HasForeignKey(d => d.CountryGroupID);
            this.HasOptional(t => t.ServiceProvider)
                .WithMany(t => t.DeliveryCharges)
                .HasForeignKey(d => d.ServiceProviderID);
            this.HasOptional(t => t.Country)
                .WithMany(t => t.DeliveryCharges)
                .HasForeignKey(d => d.FromCountryID);
            this.HasOptional(t => t.Country1)
                .WithMany(t => t.DeliveryCharges1)
                .HasForeignKey(d => d.ToCountryID);
            this.HasRequired(t => t.DeliveryCharge2)
                .WithOptional(t => t.DeliveryCharges1);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.DeliveryCharges)
                .HasForeignKey(d => d.DeliveryTypeID);

        }
    }
}
