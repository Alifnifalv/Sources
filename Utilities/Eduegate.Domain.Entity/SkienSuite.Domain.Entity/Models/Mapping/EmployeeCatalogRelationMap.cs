using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmployeeCatalogRelationMap : EntityTypeConfiguration<EmployeeCatalogRelation>
    {
        public EmployeeCatalogRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeCatalogRelationsIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EmployeeCatalogRelations", "catalog");
            this.Property(t => t.EmployeeCatalogRelationsIID).HasColumnName("EmployeeCatalogRelationsIID");
            this.Property(t => t.RelationTypeID).HasColumnName("RelationTypeID");
            this.Property(t => t.RelationID).HasColumnName("RelationID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.RelationType)
                .WithMany(t => t.EmployeeCatalogRelations)
                .HasForeignKey(d => d.RelationTypeID);

        }
    }
}
