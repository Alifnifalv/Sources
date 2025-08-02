using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedSearchViewMap : EntityTypeConfiguration<DataFeedSearchView>
    {
        public DataFeedSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedLogIID);

            // Properties
            this.Property(t => t.DataFeedLogIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FileName)
                .HasMaxLength(250);

            this.Property(t => t.FeedName)
                .HasMaxLength(250);

            this.Property(t => t.StatusName)
                .HasMaxLength(100);

            this.Property(t => t.RowCategory)
                .HasMaxLength(6);

            this.Property(t => t.ImportedBy)
                .HasMaxLength(510);

            // Table & Column Mappings
            this.ToTable("DataFeedSearchView", "feed");
            this.Property(t => t.DataFeedLogIID).HasColumnName("DataFeedLogIID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FeedName).HasColumnName("FeedName");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
            this.Property(t => t.ImportedBy).HasColumnName("ImportedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.companyID).HasColumnName("companyID");
        }
    }
}
