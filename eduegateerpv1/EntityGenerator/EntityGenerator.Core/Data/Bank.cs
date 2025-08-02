using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Banks", Schema = "mutual")]
    public partial class Bank
    {
        public Bank()
        {
            BankAccounts = new HashSet<BankAccount>();
            EmployeeBankDetails = new HashSet<EmployeeBankDetail>();
            FeeCollectionPaymentModeMaps = new HashSet<FeeCollectionPaymentModeMap>();
            SchoolPayerBankDetailMaps = new HashSet<SchoolPayerBankDetailMap>();
        }

        [Key]
        public long BankIID { get; set; }
        [StringLength(10)]
        public string BankCode { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ShortName { get; set; }

        [InverseProperty("Bank")]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        [InverseProperty("Bank")]
        public virtual ICollection<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        [InverseProperty("Bank")]
        public virtual ICollection<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
        [InverseProperty("Bank")]
        public virtual ICollection<SchoolPayerBankDetailMap> SchoolPayerBankDetailMaps { get; set; }
    }
}
