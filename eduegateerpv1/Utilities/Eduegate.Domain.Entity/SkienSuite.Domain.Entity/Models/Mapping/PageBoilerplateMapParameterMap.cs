using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PageBoilerplateMapParameterMap : EntityTypeConfiguration<PageBoilerplateMapParameter>
    {
        public PageBoilerplateMapParameterMap()
        {
            // Primary Key
            this.HasKey(t => t.PageBoilerplateMapParameterIID);

            // Properties
            this.Property(t => t.ParameterName)
                .HasMaxLength(50);

            this.Property(t => t.ParameterValue)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PageBoilerplateMapParameters", "cms");
            this.Property(t => t.PageBoilerplateMapParameterIID).HasColumnName("PageBoilerplateMapParameterIID");
            this.Property(t => t.PageBoilerplateMapID).HasColumnName("PageBoilerplateMapID");
            this.Property(t => t.ParameterName).HasColumnName("ParameterName");
            this.Property(t => t.ParameterValue).HasColumnName("ParameterValue");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.PageBoilerplateMap)
                .WithMany(t => t.PageBoilerplateMapParameters)
                .HasForeignKey(d => d.PageBoilerplateMapID);

        }
    }
}
