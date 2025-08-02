using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ChartOfAccountMapMap : EntityTypeConfiguration<ChartOfAccountMap>
    {
        public ChartOfAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ChartOfAccountMapIID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.AccountCode)
                .HasMaxLength(20);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ChartOfAccountMaps", "account");
            this.Property(t => t.ChartOfAccountMapIID).HasColumnName("ChartOfAccountMapIID");
            this.Property(t => t.ChartOfAccountID).HasColumnName("ChartOfAccountID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.AccountCode).HasColumnName("AccountCode");
            this.Property(t => t.IncomeOrBalance).HasColumnName("IncomeOrBalance");
            this.Property(t => t.ChartRowTypeID).HasColumnName("ChartRowTypeID");
            this.Property(t => t.NoOfBlankLines).HasColumnName("NoOfBlankLines");
            this.Property(t => t.IsNewPage).HasColumnName("IsNewPage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.ChartOfAccountMaps)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.ChartOfAccount)
                .WithMany(t => t.ChartOfAccountMaps)
                .HasForeignKey(d => d.ChartOfAccountID);
            this.HasOptional(t => t.ChartRowType)
                .WithMany(t => t.ChartOfAccountMaps)
                .HasForeignKey(d => d.ChartRowTypeID);

        }
    }
}
