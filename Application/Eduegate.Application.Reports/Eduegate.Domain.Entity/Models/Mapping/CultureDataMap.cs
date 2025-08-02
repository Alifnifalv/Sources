using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CultureDataMap : EntityTypeConfiguration<CultureData>
    {
        public CultureDataMap()
        {
            // Primary Key
            this.HasKey(t => t.CultureDataIID);

            // Properties
            this.Property(t => t.TableName)
                .HasMaxLength(50);

            this.Property(t => t.ColumnName)
                .HasMaxLength(50);

            this.Property(t => t.Data)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CultureDatas", "setting");
            this.Property(t => t.CultureDataIID).HasColumnName("CultureDataIID");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.ColumnName).HasColumnName("ColumnName");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Culture)
                .WithMany(t => t.CultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
