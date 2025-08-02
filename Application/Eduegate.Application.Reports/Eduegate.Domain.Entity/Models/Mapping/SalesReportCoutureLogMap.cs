using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SalesReportCoutureLogMap : EntityTypeConfiguration<SalesReportCoutureLog>
    {
        public SalesReportCoutureLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SalesReportCoutureLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.LogDate).HasColumnName("LogDate");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
        }
    }
}
