using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSlabMultiPriceProcessMap : EntityTypeConfiguration<CustomerSlabMultiPriceProcess>
    {
        public CustomerSlabMultiPriceProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSlabMultiPriceProcessID);

            // Properties
            this.Property(t => t.LogKey)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CustomerSlabMultiPriceProcess");
            this.Property(t => t.CustomerSlabMultiPriceProcessID).HasColumnName("CustomerSlabMultiPriceProcessID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.LogKey).HasColumnName("LogKey");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.TotalProducts).HasColumnName("TotalProducts");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.FinishedProducts).HasColumnName("FinishedProducts");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
        }
    }
}
