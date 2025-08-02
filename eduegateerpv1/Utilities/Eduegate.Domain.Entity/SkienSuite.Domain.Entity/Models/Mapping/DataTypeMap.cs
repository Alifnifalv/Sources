using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataTypeMap : EntityTypeConfiguration<DataType>
    {
        public DataTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DataTypeID);

            // Properties
            this.Property(t => t.DateTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DataTypes", "setting");
            this.Property(t => t.DataTypeID).HasColumnName("DataTypeID");
            this.Property(t => t.DateTypeName).HasColumnName("DateTypeName");
        }
    }
}
