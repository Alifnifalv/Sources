using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eduegate.Domain.Entity.Models.Clinic;

namespace Eduegate.Domain.Entity.Models.Mapping.Clinic
{
    public class AxImageMap : EntityTypeConfiguration<AxImage>
    {
        public AxImageMap()
        {
            // Primary Key
            this.HasKey(t => t.AxId);

            // Properties
            this.Property(t => t.Id)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(250);

            this.Property(t => t.border)
                .HasMaxLength(10);

            this.Property(t => t.width)
                .HasMaxLength(10);

            this.Property(t => t.height)
                .HasMaxLength(10);

            this.Property(t => t.css)
                .HasMaxLength(50);

            this.Property(t => t.ToolTip)
                .HasMaxLength(255);

            this.Property(t => t.ImageFormat)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("AxImage");
            this.Property(t => t.AxId).HasColumnName("AxId");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.border).HasColumnName("border");
            this.Property(t => t.width).HasColumnName("width");
            this.Property(t => t.height).HasColumnName("height");
            this.Property(t => t.css).HasColumnName("class");
            this.Property(t => t.Edit).HasColumnName("Edit");
            this.Property(t => t.CheckedOutByUser).HasColumnName("CheckedOutByUser");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.ToolTip).HasColumnName("ToolTip");
            this.Property(t => t.ImageFormat).HasColumnName("ImageFormat");

            // Relationships
            this.HasOptional(t => t.AxDocument)
                .WithMany(t => t.AxImages)
                .HasForeignKey(d => d.DocumentID);

        }
    }
}
