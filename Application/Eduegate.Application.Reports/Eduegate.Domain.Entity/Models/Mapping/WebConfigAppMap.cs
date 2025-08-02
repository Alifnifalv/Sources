using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WebConfigAppMap : EntityTypeConfiguration<WebConfigApp>
    {
        public WebConfigAppMap()
        {
            // Primary Key
            this.HasKey(t => t.ParamID);

            // Properties
            this.Property(t => t.ParamName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("WebConfigApp");
            this.Property(t => t.ParamID).HasColumnName("ParamID");
            this.Property(t => t.ParamName).HasColumnName("ParamName");
            this.Property(t => t.ParamValue).HasColumnName("ParamValue");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.Updatedon).HasColumnName("Updatedon");
        }
    }
}
