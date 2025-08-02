using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketProductMapMap : EntityTypeConfiguration<TicketProductMap>
    {
        public TicketProductMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketProductMapIID);

            // Properties
            this.Property(t => t.Narration)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TicketProductMaps", "cs");
            this.Property(t => t.TicketProductMapIID).HasColumnName("TicketProductMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.ReasonID).HasColumnName("ReasonID");
            this.Property(t => t.Narration).HasColumnName("Narration");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.TicketProductMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.TicketProductMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.TicketReason)
                .WithMany(t => t.TicketProductMaps)
                .HasForeignKey(d => d.ReasonID);
            this.HasOptional(t => t.Ticket)
                .WithMany(t => t.TicketProductMaps)
                .HasForeignKey(d => d.TicketID);

        }
    }
}
