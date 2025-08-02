using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductVisitLogMap : EntityTypeConfiguration<ProductVisitLog>
    {
        public ProductVisitLogMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.VisitOn, t.SessionID, t.IPAddress, t.IPCountry });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VisitFrom)
                .HasMaxLength(3);

            this.Property(t => t.VisitProduct)
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("ProductVisitLog");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.VisitOn).HasColumnName("VisitOn");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.IsSearch).HasColumnName("IsSearch");
            this.Property(t => t.VisitFrom).HasColumnName("VisitFrom");
            this.Property(t => t.VisitProduct).HasColumnName("VisitProduct");
        }
    }
}
