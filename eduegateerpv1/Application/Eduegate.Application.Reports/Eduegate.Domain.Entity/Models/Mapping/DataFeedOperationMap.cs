using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedOperationMap : EntityTypeConfiguration<DataFeedOperation>
    {
        public DataFeedOperationMap()
        {
            // Primary Key
            this.HasKey(t => t.OperationID);

            // Properties
            this.Property(t => t.OperationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DataFeedOperations", "feed");
            this.Property(t => t.OperationID).HasColumnName("OperationID");
            this.Property(t => t.OperationName).HasColumnName("OperationName");
        }
    }
}
