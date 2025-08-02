using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketIID);

            // Properties
            this.Property(t => t.TicketNo)
                .HasMaxLength(50);

            this.Property(t => t.Subject)
                .HasMaxLength(100);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Tickets", "cs");
            this.Property(t => t.TicketIID).HasColumnName("TicketIID");
            this.Property(t => t.TicketNo).HasColumnName("TicketNo");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Description2).HasColumnName("Description2");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.TicketStatusID).HasColumnName("TicketStatusID");
            this.Property(t => t.AssingedEmployeeID).HasColumnName("AssingedEmployeeID");
            this.Property(t => t.ManagerEmployeeID).HasColumnName("ManagerEmployeeID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.SupportAction)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.ActionID);
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.EmployeeID);
            this.HasOptional(t => t.Employee1)
                .WithMany(t => t.Tickets1)
                .HasForeignKey(d => d.ManagerEmployeeID);
            this.HasOptional(t => t.Employee2)
                .WithMany(t => t.Tickets2)
                .HasForeignKey(d => d.AssingedEmployeeID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.SupplierID);
            this.HasOptional(t => t.TicketStatus)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.TicketStatusID);

        }
    }
}
