using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class StockNotificationMap : EntityTypeConfiguration<StockNotification>
    {
        public StockNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.NotifyStockIID);

            // Properties
            this.Property(t => t.EmailID)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("StockNotifications", "inventory");
            this.Property(t => t.NotifyStockIID).HasColumnName("NotifyStockIID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.NotficationStatusID).HasColumnName("NotficationStatusID");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.StockNotifications)
                .HasForeignKey(d => d.LoginID);
            //this.HasOptional(t => t.ProductSKUMap)
            //    .WithMany(t => t.StockNotifications)
            //    .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.StockNotificationStatus)
                .WithMany(t => t.StockNotifications)
                .HasForeignKey(d => d.NotficationStatusID);

        }
    }
}
