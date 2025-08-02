using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataHistoryEntityMap : EntityTypeConfiguration<DataHistoryEntity>
    {
        public DataHistoryEntityMap()
        {
            // Primary Key
            this.HasKey(t => t.DataHistoryEntityID);

            // Properties
            this.Property(t => t.DataHistoryEntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.TableName)
                .HasMaxLength(200);

            this.Property(t => t.DBName)
                .HasMaxLength(50);

            this.Property(t => t.SchemaName)
                .HasMaxLength(50);

            this.Property(t => t.LoggerTableName)
                .HasMaxLength(200);

            this.Property(t => t.LoggerDBName)
                .HasMaxLength(50);

            this.Property(t => t.LoggerSchemaName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DataHistoryEntities", "setting");
            this.Property(t => t.DataHistoryEntityID).HasColumnName("DataHistoryEntityID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.DBName).HasColumnName("DBName");
            this.Property(t => t.SchemaName).HasColumnName("SchemaName");
            this.Property(t => t.LoggerTableName).HasColumnName("LoggerTableName");
            this.Property(t => t.LoggerDBName).HasColumnName("LoggerDBName");
            this.Property(t => t.LoggerSchemaName).HasColumnName("LoggerSchemaName");
        }
    }
}
