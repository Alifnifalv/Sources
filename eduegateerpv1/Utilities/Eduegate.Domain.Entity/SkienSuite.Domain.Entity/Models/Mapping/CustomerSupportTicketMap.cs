using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSupportTicketMap : EntityTypeConfiguration<CustomerSupportTicket>
    {
        public CustomerSupportTicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketIID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Telephone)
                .HasMaxLength(100);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.TransactionNo)
                .HasMaxLength(50);

            this.Property(t => t.Comments)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerSupportTicket", "cms");
            this.Property(t => t.TicketIID).HasColumnName("TicketIID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.TransactionNo).HasColumnName("TransactionNo");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.CustomerSupportTickets)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
