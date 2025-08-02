using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ExtraTimeTypeMap : EntityTypeConfiguration<ExtraTimeType>
    {
        public ExtraTimeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ExtraTimeTypeID);

            // Properties
            this.Property(t => t.ExtraTimeTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ExtraTimeTypes", "saloon");
            this.Property(t => t.ExtraTimeTypeID).HasColumnName("ExtraTimeTypeID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
