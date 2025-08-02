using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PageBoilerplateMapParameterCultureDataMap : EntityTypeConfiguration<PageBoilerplateMapParameterCultureData>
    {
        public PageBoilerplateMapParameterCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.PageBoilerplateMapParameterID });

            // Properties
            this.Property(t => t.PageBoilerplateMapParameterID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ParameterValue)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PageBoilerplateMapParameterCultureDatas", "cms");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.PageBoilerplateMapParameterID).HasColumnName("PageBoilerplateMapParameterID");
            this.Property(t => t.ParameterValue).HasColumnName("ParameterValue");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.PageBoilerplateMapParameterCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.PageBoilerplateMapParameter)
                .WithMany(t => t.PageBoilerplateMapParameterCultureDatas)
                .HasForeignKey(d => d.PageBoilerplateMapParameterID);

        }
    }
}
