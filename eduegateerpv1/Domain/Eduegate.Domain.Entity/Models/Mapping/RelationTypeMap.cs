using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class RelationTypeMap : EntityTypeConfiguration<RelationType>
    {
        public RelationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.RelationTypeID);

            // Properties
            this.Property(t => t.RelationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RelationName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("RelationTypes", "catalog");
            this.Property(t => t.RelationTypeID).HasColumnName("RelationTypeID");
            this.Property(t => t.RelationName).HasColumnName("RelationName");
        }
    }
}
