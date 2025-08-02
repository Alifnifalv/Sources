using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Logging.Models.Mapping
{
    public class OperationTypeMap : EntityTypeConfiguration<OperationType>
    {
        public OperationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.OperationTypeID);

            // Properties
            this.Property(t => t.OperationTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OperationType", "mutual");
            this.Property(t => t.OperationTypeID).HasColumnName("OperationTypeID");
            this.Property(t => t.OperationTypeName).HasColumnName("OperationTypeName");
        }
    }
}
