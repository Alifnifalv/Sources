using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EmployeeBankDetailMap : EntityTypeConfiguration<EmployeeBankDetail>
    {
        public EmployeeBankDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeBankIID);

            // Properties
            this.Property(t => t.EmployeeBankIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BankName)
                .HasMaxLength(50);

            this.Property(t => t.BankDetails)
                .HasMaxLength(150);

            this.Property(t => t.AccountHolderName)
                .HasMaxLength(50);

            this.Property(t => t.AccountNo)
                .HasMaxLength(50);

            this.Property(t => t.IBAN)
                .HasMaxLength(50);

            this.Property(t => t.SwiftCode)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EmployeeBankDetails", "payroll");
            this.Property(t => t.EmployeeBankIID).HasColumnName("EmployeeBankIID");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankDetails).HasColumnName("BankDetails");
            this.Property(t => t.AccountHolderName).HasColumnName("AccountHolderName");
            this.Property(t => t.AccountNo).HasColumnName("AccountNo");
            this.Property(t => t.IBAN).HasColumnName("IBAN");
            this.Property(t => t.SwiftCode).HasColumnName("SwiftCode");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
