using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFormatTypeMap : EntityTypeConfiguration<DataFormatType>
    {
        public DataFormatTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFormatTypeID);

            // Properties
            this.Property(t => t.DataFormatTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("DataFormatTypes", "setting");
            this.Property(t => t.DataFormatTypeID).HasColumnName("DataFormatTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
