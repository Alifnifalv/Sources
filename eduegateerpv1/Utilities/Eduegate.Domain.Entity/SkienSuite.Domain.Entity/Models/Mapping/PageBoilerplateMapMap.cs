using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PageBoilerplateMapMap : EntityTypeConfiguration<PageBoilerplateMap>
    {
        public PageBoilerplateMapMap()
        {
            // Primary Key
            this.HasKey(t => t.PageBoilerplateMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PageBoilerplateMaps", "cms");
            this.Property(t => t.PageBoilerplateMapIID).HasColumnName("PageBoilerplateMapIID");
            this.Property(t => t.PageID).HasColumnName("PageID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.BoilerplateID).HasColumnName("BoilerplateID");
            this.Property(t => t.SerialNumber).HasColumnName("SerialNumber");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.BoilerPlate)
                .WithMany(t => t.PageBoilerplateMaps)
                .HasForeignKey(d => d.BoilerplateID);
            this.HasOptional(t => t.Page)
                .WithMany(t => t.PageBoilerplateMaps)
                .HasForeignKey(d => d.PageID);

        }
    }
}
