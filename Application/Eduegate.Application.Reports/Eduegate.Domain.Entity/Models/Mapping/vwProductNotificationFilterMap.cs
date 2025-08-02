using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductNotificationFilterMap : EntityTypeConfiguration<vwProductNotificationFilter>
    {
        public vwProductNotificationFilterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ProductPartNo)
                .HasMaxLength(20);

            this.Property(t => t.RequestedOn)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("vwProductNotificationFilter");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductPartNo).HasColumnName("ProductPartNo");
            this.Property(t => t.RequestedOn).HasColumnName("RequestedOn");
        }
    }
}
