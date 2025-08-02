using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ChartRowTypeMap : EntityTypeConfiguration<ChartRowType>
    {
        public ChartRowTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ChartRowTypeID);

            // Properties
            this.Property(t => t.ChartRowTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ChartRowTypes", "account");
            this.Property(t => t.ChartRowTypeID).HasColumnName("ChartRowTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
