using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedDetailViewMap : EntityTypeConfiguration<DataFeedDetailView>
    {
        public DataFeedDetailViewMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedLogDetailIID);

            // Properties
            this.Property(t => t.DataFeedLogDetailIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DataFeedDetailView", "feed");
            this.Property(t => t.DataFeedLogDetailIID).HasColumnName("DataFeedLogDetailIID");
            this.Property(t => t.ErrorMessage).HasColumnName("ErrorMessage");
            this.Property(t => t.DataFeedLogIID).HasColumnName("DataFeedLogIID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
