using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WebConfigMap : EntityTypeConfiguration<WebConfig>
    {
        public WebConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.ParamName);

            // Properties
            this.Property(t => t.ParamName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ParamValue)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("WebConfig");
            this.Property(t => t.ParamName).HasColumnName("ParamName");
            this.Property(t => t.ParamValue).HasColumnName("ParamValue");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.Updatedon).HasColumnName("Updatedon");
        }
    }
}
