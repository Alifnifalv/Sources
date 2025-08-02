using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UIControlTypeMap : EntityTypeConfiguration<UIControlType>
    {
        public UIControlTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.UIControlTypeID);

            // Properties
            this.Property(t => t.ControlName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UIControlTypes", "setting");
            this.Property(t => t.UIControlTypeID).HasColumnName("UIControlTypeID");
            this.Property(t => t.ControlName).HasColumnName("ControlName");
        }
    }
}
