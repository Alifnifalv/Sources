using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SalesRelationshipTypeMap : EntityTypeConfiguration<SalesRelationshipType>
    {
        public SalesRelationshipTypeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.SalesRelationTypeID });

            // Properties
            this.Property(t => t.RelationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SalesRelationshipType", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.SalesRelationTypeID).HasColumnName("SalesRelationTypeID");
            this.Property(t => t.RelationName).HasColumnName("RelationName");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.SalesRelationshipTypes)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
