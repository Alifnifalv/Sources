using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedTableColumnMap : EntityTypeConfiguration<DataFeedTableColumn>
    {
        public DataFeedTableColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedTableColumnID);

            // Properties
            this.Property(t => t.DataFeedTableColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PhysicalColumnName)
                .HasMaxLength(250);

            this.Property(t => t.DisplayColumnName)
                .HasMaxLength(250);

            this.Property(t => t.DataType)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("DataFeedTableColumns", "feed");
            this.Property(t => t.DataFeedTableColumnID).HasColumnName("DataFeedTableColumnID");
            this.Property(t => t.DataFeedTableID).HasColumnName("DataFeedTableID");
            this.Property(t => t.PhysicalColumnName).HasColumnName("PhysicalColumnName");
            this.Property(t => t.DisplayColumnName).HasColumnName("DisplayColumnName");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.IsPrimaryKey).HasColumnName("IsPrimaryKey");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");

            // Relationships
            this.HasRequired(t => t.DataFeedTable)
                .WithMany(t => t.DataFeedTableColumns)
                .HasForeignKey(d => d.DataFeedTableID);

        }
    }
}
