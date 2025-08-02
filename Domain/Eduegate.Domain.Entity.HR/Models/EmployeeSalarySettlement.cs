using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Domain.Entity.HR
{
    [Table("EmployeeSalarySettlements", Schema = "payroll")]
    public partial class EmployeeSalarySettlement
    {
        [Key]
        public long EmployeeSalarySettlementIID { get; set; }
        public long? EmployeeID { get; set; }
        public long? EmployeeSalaryStructureID { get; set; }
        public byte? EmployeeSettlementTypeID { get; set; }
        [StringLength(25)]
        public string EmployeeSettlementNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EmployeeSettlementDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LeaveSalaryAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Gratuity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LeaveDueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LeaveDueFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LeaveStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LeaveEndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SalaryCalculationDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofSalaryDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofLeaveSalaryDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AnnualLeaveEntitilements { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? EarnedLeave { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LossofPay { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofDaysInTheMonth { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofDaysInTheMonthLS { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SalaryComponentAmount { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Notes { get; set; }
        public byte? EmployeeSettlementStatusID { get; set; }
        public long? ReportContentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsVerified { get; set; }
        public decimal? EndofServiceDaysPerYear { get; set; }
        public decimal? NoofDaysInTheMonthEoSB { get; set; }
        public System.DateTime? DateOfLeaving { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeSalarySettlements")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EmployeeSalaryStructureID")]
        [InverseProperty("EmployeeSalarySettlements")]
        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }
        [ForeignKey("EmployeeSettlementStatusID")]
        [InverseProperty("EmployeeSalarySettlements")]
        public virtual EmployeeSettlementStatus EmployeeSettlementStatus { get; set; }
        [ForeignKey("EmployeeSettlementTypeID")]
        [InverseProperty("EmployeeSalarySettlements")]
        public virtual EmployeeSettlementType EmployeeSettlementType { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeSalarySettlements")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}