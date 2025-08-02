using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedLogDetailMap : EntityTypeConfiguration<DataFeedLogDetail>
    {
        public DataFeedLogDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedLogDetailIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DataFeedLogDetails", "feed");
            this.Property(t => t.DataFeedLogDetailIID).HasColumnName("DataFeedLogDetailIID");
            this.Property(t => t.DataFeedLogID).HasColumnName("DataFeedLogID");
            this.Property(t => t.ErrorMessage).HasColumnName("ErrorMessage");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
        }
    }
}
