using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TreatmentTypeMap : EntityTypeConfiguration<TreatmentType>
    {
        public TreatmentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TreatmentTypeID);

            // Properties
            this.Property(t => t.TreatmentTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TreatmentName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("TreatmentTypes", "saloon");
            this.Property(t => t.TreatmentTypeID).HasColumnName("TreatmentTypeID");
            this.Property(t => t.TreatmentName).HasColumnName("TreatmentName");
            this.Property(t => t.TreatmentGroupID).HasColumnName("TreatmentGroupID");

            // Relationships
            this.HasRequired(t => t.TreatmentGroup)
                .WithMany(t => t.TreatmentTypes)
                .HasForeignKey(d => d.TreatmentGroupID);

        }
    }
}
