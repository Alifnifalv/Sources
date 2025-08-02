using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFormatMap : EntityTypeConfiguration<DataFormat>
    {
        public DataFormatMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFormatID);

            // Properties
            this.Property(t => t.DataFormatID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Format)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DataFormats", "setting");
            this.Property(t => t.DataFormatID).HasColumnName("DataFormatID");
            this.Property(t => t.DataFormatTypeID).HasColumnName("DataFormatTypeID");
            this.Property(t => t.Format).HasColumnName("Format");
            this.Property(t => t.IsDefaultFormat).HasColumnName("IsDefaultFormat");

            // Relationships
            this.HasOptional(t => t.DataFormatType)
                .WithMany(t => t.DataFormats)
                .HasForeignKey(d => d.DataFormatTypeID);

        }
    }
}
