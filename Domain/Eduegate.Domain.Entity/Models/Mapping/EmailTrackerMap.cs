using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmailTrackerMap : EntityTypeConfiguration<EmailTracker>
    {
        public EmailTrackerMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            this.Property(t => t.EmailID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EmailTracker");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.Dated).HasColumnName("Dated");
        }
    }
}
