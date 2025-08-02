using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountBehavoirMap : EntityTypeConfiguration<AccountBehavoir>
    {
        public AccountBehavoirMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountBehavoirID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AccountBehavoirs", "account");
            this.Property(t => t.AccountBehavoirID).HasColumnName("AccountBehavoirID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
