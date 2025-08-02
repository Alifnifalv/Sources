using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OperationTypeMap : EntityTypeConfiguration<OperationType>
    {
        public OperationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.OperationTypeID);

            // Properties
            this.Property(t => t.OperationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OperationTypes", "sync");
            this.Property(t => t.OperationTypeID).HasColumnName("OperationTypeID");
            this.Property(t => t.OperationName).HasColumnName("OperationName");
        }
    }
}
