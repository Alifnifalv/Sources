using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoGrnDetailMap : EntityTypeConfiguration<IntlPoGrnDetail>
    {
        public IntlPoGrnDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoGrnDetailsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("IntlPoGrnDetails");
            this.Property(t => t.IntlPoGrnDetailsID).HasColumnName("IntlPoGrnDetailsID");
            this.Property(t => t.RefIntlPoGrnMasterID).HasColumnName("RefIntlPoGrnMasterID");
            this.Property(t => t.RefIntlPoOrderDetailsID).HasColumnName("RefIntlPoOrderDetailsID");
            this.Property(t => t.QtyReceived).HasColumnName("QtyReceived");
            this.Property(t => t.QtyShipped).HasColumnName("QtyShipped");

            // Relationships
            this.HasRequired(t => t.IntlPoGrnMaster)
                .WithMany(t => t.IntlPoGrnDetails)
                .HasForeignKey(d => d.RefIntlPoGrnMasterID);
            this.HasRequired(t => t.IntlPoOrderDetail)
                .WithMany(t => t.IntlPoGrnDetails)
                .HasForeignKey(d => d.RefIntlPoOrderDetailsID);

        }
    }
}
