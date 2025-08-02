using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDayDealMap : EntityTypeConfiguration<ProductDayDeal>
    {
        public ProductDayDealMap()
        {
            // Primary Key
            this.HasKey(t => t.DealID);

            // Properties
            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ProductDayDeal");
            this.Property(t => t.DealID).HasColumnName("DealID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.DealPrice).HasColumnName("DealPrice");
            this.Property(t => t.DealQty).HasColumnName("DealQty");
            this.Property(t => t.DealPickUpShowroom).HasColumnName("DealPickUpShowroom");
            this.Property(t => t.DealMaxOrderQty).HasColumnName("DealMaxOrderQty");
            this.Property(t => t.DealMaxOrderQtyVerified).HasColumnName("DealMaxOrderQtyVerified");
            this.Property(t => t.DealMaxCustomerQty).HasColumnName("DealMaxCustomerQty");
            this.Property(t => t.DealMaxCustomerQtyDuration).HasColumnName("DealMaxCustomerQtyDuration");
            this.Property(t => t.StartDateTime).HasColumnName("StartDateTime");
            this.Property(t => t.EndDateTime).HasColumnName("EndDateTime");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ContinueDeal).HasColumnName("ContinueDeal");
            this.Property(t => t.ContinueDealEndDateTime).HasColumnName("ContinueDealEndDateTime");
            this.Property(t => t.CurrentPrice).HasColumnName("CurrentPrice");
            this.Property(t => t.DealPosition).HasColumnName("DealPosition");
        }
    }
}
