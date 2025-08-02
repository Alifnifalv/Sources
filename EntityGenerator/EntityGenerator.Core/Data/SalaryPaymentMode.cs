using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryPaymentModes", Schema = "payroll")]
    public partial class SalaryPaymentMode
    {
        public SalaryPaymentMode()
        {
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructures = new HashSet<SalaryStructure>();
        }

        [Key]
        public int SalaryPaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentName { get; set; }
        public byte? PyamentModeTypeID { get; set; }

        [InverseProperty("PaymentMode")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
        [InverseProperty("PaymentMode")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        [InverseProperty("PaymentMode")]
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }
    }
}
