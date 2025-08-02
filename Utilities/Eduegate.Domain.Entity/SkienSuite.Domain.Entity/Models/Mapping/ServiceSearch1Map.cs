using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceSearch1Map : EntityTypeConfiguration<ServiceSearch1>
    {
        public ServiceSearch1Map()
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

            this.Property(t => t.TreatmentName)
                .HasMaxLength(200);

            this.Property(t => t.ServiceAvailable)
                .HasMaxLength(100);

            this.Property(t => t.ExtraTimeTypes)
                .HasMaxLength(100);

            this.Property(t => t.GroupName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ServiceSearch", "saloon");
            this.Property(t => t.ServiceIID).HasColumnName("ServiceIID");
            this.Property(t => t.ParentServiceID).HasColumnName("ParentServiceID");
            this.Property(t => t.ServiceCode).HasColumnName("ServiceCode");
            this.Property(t => t.ServiceName).HasColumnName("ServiceName");
            this.Property(t => t.ServiceDescription).HasColumnName("ServiceDescription");
            this.Property(t => t.TreatmentTypeID).HasColumnName("TreatmentTypeID");
            this.Property(t => t.TreatmentName).HasColumnName("TreatmentName");
            this.Property(t => t.ServiceAvailableID).HasColumnName("ServiceAvailableID");
            this.Property(t => t.ServiceAvailable).HasColumnName("ServiceAvailable");
            this.Property(t => t.PricingTypeID).HasColumnName("PricingTypeID");
            this.Property(t => t.ExtraTimeTypeID).HasColumnName("ExtraTimeTypeID");
            this.Property(t => t.ExtraTimeTypes).HasColumnName("ExtraTimeTypes");
            this.Property(t => t.ExtratimeDuration).HasColumnName("ExtratimeDuration");
            this.Property(t => t.ServiceGroupID).HasColumnName("ServiceGroupID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Updated).HasColumnName("Updated");
        }
    }
}
