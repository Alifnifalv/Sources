using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ErrorTraceMap : EntityTypeConfiguration<ErrorTrace>
    {
        public ErrorTraceMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.ERROR_MESSAGE)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.ERROR_LINE)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SP_Name)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("ErrorTrace");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ERROR_MESSAGE).HasColumnName("ERROR_MESSAGE");
            this.Property(t => t.ERROR_LINE).HasColumnName("ERROR_LINE");
            this.Property(t => t.SP_Name).HasColumnName("SP_Name");
            this.Property(t => t.ErrorOn).HasColumnName("ErrorOn");
        }
    }
}
