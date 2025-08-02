using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TreatmentGroupMap : EntityTypeConfiguration<TreatmentGroup>
    {
        public TreatmentGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.TreatmentGroupID);

            // Properties
            this.Property(t => t.TreatmentGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("TreatmentGroups", "saloon");
            this.Property(t => t.TreatmentGroupID).HasColumnName("TreatmentGroupID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
