using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class FilterColumnConditionMapMap : EntityTypeConfiguration<FilterColumnConditionMap>
    {
        public FilterColumnConditionMapMap()
        {
            // Primary Key
            this.HasKey(t => t.FilterColumnConditionMapID);

            // Properties
            this.Property(t => t.FilterColumnConditionMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FilterColumnConditionMaps", "setting");
            this.Property(t => t.FilterColumnConditionMapID).HasColumnName("FilterColumnConditionMapID");
            this.Property(t => t.DataTypeID).HasColumnName("DataTypeID");
            this.Property(t => t.FilterColumnID).HasColumnName("FilterColumnID");
            this.Property(t => t.ConidtionID).HasColumnName("ConidtionID");

            // Relationships
            this.HasOptional(t => t.Condition)
                .WithMany(t => t.FilterColumnConditionMaps)
                .HasForeignKey(d => d.ConidtionID);
            this.HasOptional(t => t.DataType)
                .WithMany(t => t.FilterColumnConditionMaps)
                .HasForeignKey(d => d.DataTypeID);
            this.HasOptional(t => t.FilterColumn)
                .WithMany(t => t.FilterColumnConditionMaps)
                .HasForeignKey(d => d.FilterColumnID);

        }
    }
}
