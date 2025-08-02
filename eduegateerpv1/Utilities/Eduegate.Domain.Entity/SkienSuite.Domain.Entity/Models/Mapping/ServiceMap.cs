using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceIID);

            // Properties
            this.Property(t => t.ServiceIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ServiceCode)
                .HasMaxLength(100);

            this.Property(t => t.ServiceName)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("Services", "saloon");
            this.Property(t => t.ServiceIID).HasColumnName("ServiceIID");
            this.Property(t => t.ParentServiceID).HasColumnName("ParentServiceID");
            this.Property(t => t.ServiceCode).HasColumnName("ServiceCode");
            this.Property(t => t.ServiceName).HasColumnName("ServiceName");
            this.Property(t => t.ServiceDescription).HasColumnName("ServiceDescription");
            this.Property(t => t.ServiceGroupID).HasColumnName("ServiceGroupID");
            this.Property(t => t.TreatmentTypeID).HasColumnName("TreatmentTypeID");
            this.Property(t => t.ServiceAvailableID).HasColumnName("ServiceAvailableID");
            this.Property(t => t.PricingTypeID).HasColumnName("PricingTypeID");
            this.Property(t => t.ExtraTimeTypeID).HasColumnName("ExtraTimeTypeID");
            this.Property(t => t.ExtratimeDuration).HasColumnName("ExtratimeDuration");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Updated).HasColumnName("Updated");

            // Relationships
            this.HasOptional(t => t.ExtraTimeType)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.ExtraTimeTypeID);
            this.HasOptional(t => t.PricingType)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.PricingTypeID);
            this.HasOptional(t => t.ServiceAvailable)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.ServiceAvailableID);
            this.HasOptional(t => t.ServiceGroup)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.ServiceGroupID);
            this.HasOptional(t => t.Service1)
                .WithMany(t => t.Services1)
                .HasForeignKey(d => d.ParentServiceID);
            this.HasOptional(t => t.TreatmentType)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.TreatmentTypeID);

        }
    }
}
