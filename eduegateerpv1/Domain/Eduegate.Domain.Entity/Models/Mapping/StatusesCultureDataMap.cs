using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class StatusesCultureDataMap : EntityTypeConfiguration<StatusesCultureData>
    {
        public StatusesCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.StatusID });

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("StatusesCultureDatas", "mutual");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.StatusesCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.Status)
                .WithMany(t => t.StatusesCultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
