namespace Eduegate.Domain.Entity.HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeBankDetails", Schema = "payroll")]
    public partial class EmployeeBankDetail
    {
        [Key]
        public long EmployeeBankIID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(20)]
        public string AccountNo { get; set; }

        public long? BankID { get; set; }

        [StringLength(20)]
        public string IBAN { get; set; }

        [StringLength(20)]
        public string SwiftCode { get; set; }

        [StringLength(50)]
        public string AccountHolderName { get; set; }

        [StringLength(100)]
        public string BankDetails { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
