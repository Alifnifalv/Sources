using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BoilerPlateMap : EntityTypeConfiguration<BoilerPlate>
    {
        public BoilerPlateMap()
        {
            // Primary Key
            this.HasKey(t => t.BoilerPlateID);

            // Properties
            this.Property(t => t.BoilerPlateID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.Template)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ReferenceIDName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BoilerPlates", "cms");
            this.Property(t => t.BoilerPlateID).HasColumnName("BoilerPlateID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Template).HasColumnName("Template");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ReferenceIDName).HasColumnName("ReferenceIDName");
            this.Property(t => t.ReferenceIDRequired).HasColumnName("ReferenceIDRequired");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.BoilerPlate1)
                .WithOptional(t => t.BoilerPlates1);

        }
    }
}
