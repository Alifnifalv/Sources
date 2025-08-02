using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceSupportMap : EntityTypeConfiguration<ServiceSupport>
    {
        public ServiceSupportMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Comments)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.RegistrationIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RegistrationCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TicketStatus)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ServiceSupport");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.RefJobCardMasterID).HasColumnName("RefJobCardMasterID");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.RegistrationIP).HasColumnName("RegistrationIP");
            this.Property(t => t.RegistrationCountry).HasColumnName("RegistrationCountry");
            this.Property(t => t.submitdate).HasColumnName("submitdate");
            this.Property(t => t.isRead).HasColumnName("isRead");
            this.Property(t => t.TicketStatus).HasColumnName("TicketStatus");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.CreatedUserID).HasColumnName("CreatedUserID");
        }
    }
}
