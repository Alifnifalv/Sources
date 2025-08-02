using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BoilerPlateParameterMap : EntityTypeConfiguration<BoilerPlateParameter>
    {
        public BoilerPlateParameterMap()
        {
            // Primary Key
            this.HasKey(t => t.BoilerPlateParameterID);

            // Properties
            this.Property(t => t.BoilerPlateParameterID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ParameterName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("BoilerPlateParameters", "cms");
            this.Property(t => t.BoilerPlateParameterID).HasColumnName("BoilerPlateParameterID");
            this.Property(t => t.BoilerPlateID).HasColumnName("BoilerPlateID");
            this.Property(t => t.ParameterName).HasColumnName("ParameterName");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasOptional(t => t.BoilerPlate)
                .WithMany(t => t.BoilerPlateParameters)
                .HasForeignKey(d => d.BoilerPlateID);

        }
    }
}
