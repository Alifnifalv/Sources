using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSlabMultiPriceProcessIntlMap : EntityTypeConfiguration<CustomerSlabMultiPriceProcessIntl>
    {
        public CustomerSlabMultiPriceProcessIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerSlabMultiPriceProcessIntlID);

            // Properties
            this.Property(t => t.LogKey)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CustomerSlabMultiPriceProcessIntl");
            this.Property(t => t.CustomerSlabMultiPriceProcessIntlID).HasColumnName("CustomerSlabMultiPriceProcessIntlID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.LogKey).HasColumnName("LogKey");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.TotalProducts).HasColumnName("TotalProducts");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.FinishedProducts).HasColumnName("FinishedProducts");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
        }
    }
}
