using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedTableMap : EntityTypeConfiguration<DataFeedTable>
    {
        public DataFeedTableMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedTableID);

            // Properties
            this.Property(t => t.DataFeedTableID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TableName)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("DataFeedTables", "feed");
            this.Property(t => t.DataFeedTableID).HasColumnName("DataFeedTableID");
            this.Property(t => t.DataFeedTypeID).HasColumnName("DataFeedTypeID");
            this.Property(t => t.TableName).HasColumnName("TableName");

            // Relationships
            this.HasOptional(t => t.DataFeedType)
                .WithMany(t => t.DataFeedTables)
                .HasForeignKey(d => d.DataFeedTypeID);

        }
    }
}
