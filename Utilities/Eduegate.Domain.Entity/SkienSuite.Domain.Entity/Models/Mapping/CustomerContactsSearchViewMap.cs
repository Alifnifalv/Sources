using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerContactsSearchViewMap : EntityTypeConfiguration<CustomerContactsSearchView>
    {
        public CustomerContactsSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CustomerIID, t.ContactIID });

            // Properties
            this.Property(t => t.CustomerIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContactIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerCR)
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .HasMaxLength(255);

            this.Property(t => t.LastName)
                .HasMaxLength(255);

            this.Property(t => t.MiddleName)
                .HasMaxLength(255);

            this.Property(t => t.AddressName)
                .HasMaxLength(500);

            this.Property(t => t.PostalCode)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(50);

            this.Property(t => t.CivilIDNumber)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CustomerContactsSearchView", "mutual");
            this.Property(t => t.CustomerIID).HasColumnName("CustomerIID");
            this.Property(t => t.ContactIID).HasColumnName("ContactIID");
            this.Property(t => t.CustomerCR).HasColumnName("CustomerCR");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.CivilIDNumber).HasColumnName("CivilIDNumber");
        }
    }
}
