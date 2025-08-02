using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductNotificationMap : EntityTypeConfiguration<vwProductNotification>
    {
        public vwProductNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.RefProductID);

            // Properties
            this.Property(t => t.RequestedOn)
                .HasMaxLength(10);

            this.Property(t => t.RefProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductNotification");
            this.Property(t => t.RequestedOn).HasColumnName("RequestedOn");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
        }
    }
}
