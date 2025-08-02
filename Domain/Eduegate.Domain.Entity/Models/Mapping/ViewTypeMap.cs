using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ViewTypeMap : EntityTypeConfiguration<ViewType>
    {
        public ViewTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ViewTypeID);

            // Properties
            this.Property(t => t.ViewTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ViewTypes", "setting");
            this.Property(t => t.ViewTypeID).HasColumnName("ViewTypeID");
            this.Property(t => t.ViewTypeName).HasColumnName("ViewTypeName");
        }
    }
}
