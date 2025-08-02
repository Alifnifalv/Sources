using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerJustAskMap : EntityTypeConfiguration<CustomerJustAsk>
    {
        public CustomerJustAskMap()
        {
            // Primary Key
            this.HasKey(t => t.JustAskIID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Telephone)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
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
            this.ToTable("CustomerJustAsk", "cms");
            this.Property(t => t.JustAskIID).HasColumnName("JustAskIID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.CustomerJustAsks)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
