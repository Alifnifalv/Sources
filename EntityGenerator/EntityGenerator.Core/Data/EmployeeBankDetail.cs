using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeBankDetails", Schema = "payroll")]
    public partial class EmployeeBankDetail
    {
        [Key]
        public long EmployeeBankIID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(20)]
        public string AccountNo { get; set; }
        public long? BankID { get; set; }
        [StringLength(50)]
        public string IBAN { get; set; }
        [StringLength(20)]
        public string SwiftCode { get; set; }
        [StringLength(50)]
        public string AccountHolderName { get; set; }
        [StringLength(100)]
        public string BankDetails { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BankID")]
        [InverseProperty("EmployeeBankDetails")]
        public virtual Bank Bank { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeBankDetails")]
        public virtual Employee Employee { get; set; }
    }
}
