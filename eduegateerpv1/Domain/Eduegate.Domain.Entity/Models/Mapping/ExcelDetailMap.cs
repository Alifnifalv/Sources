using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ExcelDetailMap : EntityTypeConfiguration<ExcelDetail>
    {
        public ExcelDetailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BatchNo, t.RefCategoryColumnID, t.ProductID });

            // Properties
            this.Property(t => t.BatchNo)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.RefCategoryColumnID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProductID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Valen)
                .HasMaxLength(150);

            this.Property(t => t.Valar)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("ExcelDetails");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.RefCategoryColumnID).HasColumnName("RefCategoryColumnID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Valen).HasColumnName("Valen");
            this.Property(t => t.Valar).HasColumnName("Valar");
        }
    }
}
