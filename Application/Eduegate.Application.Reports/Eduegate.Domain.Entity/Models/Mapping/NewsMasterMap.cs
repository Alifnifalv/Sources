using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsMasterMap : EntityTypeConfiguration<NewsMaster>
    {
        public NewsMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsID);

            // Properties
            this.Property(t => t.ReferenceKey)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.NewsTitleEn)
                .HasMaxLength(255);

            this.Property(t => t.NewsTitleAr)
                .HasMaxLength(255);

            this.Property(t => t.ImageName)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("NewsMaster");
            this.Property(t => t.NewsID).HasColumnName("NewsID");
            this.Property(t => t.ReferenceKey).HasColumnName("ReferenceKey");
            this.Property(t => t.NewsTitleEn).HasColumnName("NewsTitleEn");
            this.Property(t => t.NewsTitleAr).HasColumnName("NewsTitleAr");
            this.Property(t => t.DetailsEn).HasColumnName("DetailsEn");
            this.Property(t => t.DetailsAr).HasColumnName("DetailsAr");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.Dated).HasColumnName("Dated");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
