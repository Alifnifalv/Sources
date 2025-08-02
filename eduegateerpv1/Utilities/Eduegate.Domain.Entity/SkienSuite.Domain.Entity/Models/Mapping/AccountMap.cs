using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountID);

            // Properties
            this.Property(t => t.Alias)
                .HasMaxLength(30);

            this.Property(t => t.AccountName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ChildAliasPrefix)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Accounts", "account");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.AccountName).HasColumnName("AccountName");
            this.Property(t => t.ParentAccountID).HasColumnName("ParentAccountID");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.AccountBehavoirID).HasColumnName("AccountBehavoirID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ChildAliasPrefix).HasColumnName("ChildAliasPrefix");
            this.Property(t => t.ChildLastID).HasColumnName("ChildLastID");

            // Relationships
            this.HasOptional(t => t.AccountBehavoir)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.AccountBehavoirID);
            this.HasOptional(t => t.Account1)
                .WithMany(t => t.Accounts1)
                .HasForeignKey(d => d.ParentAccountID);
            this.HasOptional(t => t.Group)
                .WithMany(t => t.Accounts)
                .HasForeignKey(d => d.GroupID);

        }
    }
}
