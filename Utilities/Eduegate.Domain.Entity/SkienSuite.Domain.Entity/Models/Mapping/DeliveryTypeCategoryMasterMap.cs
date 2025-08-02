using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeCategoryMasterMap : EntityTypeConfiguration<DeliveryTypeCategoryMaster>
    {
        public DeliveryTypeCategoryMasterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefCategoryID, t.RefDeliveryTypeID });

            // Properties
            this.Property(t => t.RefCategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefDeliveryTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DeliveryTypeCategoryMaster", "cms");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefDeliveryTypeID).HasColumnName("RefDeliveryTypeID");

            // Relationships
            this.HasRequired(t => t.DeliveryTypeMaster)
                .WithMany(t => t.DeliveryTypeCategoryMasters)
                .HasForeignKey(d => d.RefDeliveryTypeID);

        }
    }
}
