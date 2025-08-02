using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UIControlValidationMap : EntityTypeConfiguration<UIControlValidation>
    {
        public UIControlValidationMap()
        {
            // Primary Key
            this.HasKey(t => t.UIControlValidationID);

            // Properties
            this.Property(t => t.ValidationName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UIControlValidations", "setting");
            this.Property(t => t.UIControlValidationID).HasColumnName("UIControlValidationID");
            this.Property(t => t.ValidationName).HasColumnName("ValidationName");
        }
    }
}
