using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoRequestMap : EntityTypeConfiguration<IntlPoRequest>
    {
        public IntlPoRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoRequestID);

            // Properties
            this.Property(t => t.ProductPartNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.RequestedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RequestStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoRequest");
            this.Property(t => t.IntlPoRequestID).HasColumnName("IntlPoRequestID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductPartNo).HasColumnName("ProductPartNo");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.QtyActualRequested).HasColumnName("QtyActualRequested");
            this.Property(t => t.QtyRequested).HasColumnName("QtyRequested");
            this.Property(t => t.QtyOrdered).HasColumnName("QtyOrdered");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.RequestedBy).HasColumnName("RequestedBy");
            this.Property(t => t.RequesterID).HasColumnName("RequesterID");
            this.Property(t => t.RequestStatus).HasColumnName("RequestStatus");
            this.Property(t => t.RefProductManagerID).HasColumnName("RefProductManagerID");
        }
    }
}
