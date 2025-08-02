using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ArabicReportStatusLogMap : EntityTypeConfiguration<ArabicReportStatusLog>
    {
        public ArabicReportStatusLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ArabicStatusLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ArabicReportStatusLog");
            this.Property(t => t.ArabicStatusLogID).HasColumnName("ArabicStatusLogID");
            this.Property(t => t.ActiveProducts).HasColumnName("ActiveProducts");
            this.Property(t => t.ActiveProductsNoArName).HasColumnName("ActiveProductsNoArName");
            this.Property(t => t.ActiveProductsNoArDesc).HasColumnName("ActiveProductsNoArDesc");
            this.Property(t => t.ActiveProductsNoArDetails).HasColumnName("ActiveProductsNoArDetails");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
