using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedStatusMap : EntityTypeConfiguration<DataFeedStatus>
    {
        public DataFeedStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedStatusID);

            // Properties
            this.Property(t => t.DataFeedStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DataFeedStatuses", "feed");
            this.Property(t => t.DataFeedStatusID).HasColumnName("DataFeedStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
