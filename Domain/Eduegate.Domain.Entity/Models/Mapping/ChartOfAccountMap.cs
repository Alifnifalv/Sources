using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ChartOfAccountMap : EntityTypeConfiguration<ChartOfAccount>
    {
        public ChartOfAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.ChartOfAccountIID);

            // Properties
            this.Property(t => t.ChartName)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ChartOfAccounts", "account");
            this.Property(t => t.ChartOfAccountIID).HasColumnName("ChartOfAccountIID");
            this.Property(t => t.ChartName).HasColumnName("ChartName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
