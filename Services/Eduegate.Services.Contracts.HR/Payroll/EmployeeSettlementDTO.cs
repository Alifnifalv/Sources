using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeSettlementDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public EmployeeSettlementDTO()
        {

            SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();
            Employee = new KeyValueDTO();
            EmployeeSalaryStructure = new KeyValueDTO();
            SalarySlipDTOs = new List<SalarySlipDTO>();
        }
        [DataMember]
        public byte? EmployeeTypeID { get; set; }

        [DataMember]
        public long EmployeeSalarySettlementIID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }

        [DataMember]
        public int? LeaveSalaryComponentID { get; set; }

        [DataMember]
        public int? GratuityComponentID { get; set; }

        [DataMember]
        public int? LoanComponentID { get; set; }

        [DataMember]
        public int? AdvanceComponentID { get; set; }

        [DataMember]
        public System.DateTime? DateOfJoining { get; set; }

        [DataMember]
        public byte? EmployeeSettlementTypeID { get; set; }

        [DataMember]
        public string EmployeeSettlementNo { get; set; }

        [DataMember]
        public System.DateTime? EmployeeSettlementDate { get; set; }

        [DataMember]
        public System.DateTime? LeaveDueFrom { get; set; }

        [DataMember]
        public System.DateTime? LeaveStartDate { get; set; }

        [DataMember]
        public System.DateTime? LeaveEndDate { get; set; }

        [DataMember]
        public System.DateTime? SalaryCalculationDate { get; set; }

        [DataMember]
        public decimal? EndofServiceDaysPerYear { get; set; }
        [DataMember]
        public decimal? NoofDaysInTheMonthEoSB { get; set; }

        [DataMember]
        public System.DateTime? DateOfLeaving { get; set; }

        [DataMember]
        public string EmployeeSettlementDateString { get; set; }

        [DataMember]
        public string LeaveDueFromString { get; set; }

        [DataMember]
        public string LeaveStartDateString { get; set; }

        [DataMember]
        public string LeaveEndDateString { get; set; }

        [DataMember]
        public string DateOfLeavingString { get; set; }

        [DataMember]
        public string SalaryCalculationDateString { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public decimal? NoofSalaryDays { get; set; }

        [DataMember]
        public decimal? NoofLeaveSalaryDays { get; set; }

        [DataMember]
        public decimal? TotalLeaveDaysAvailable { get; set; }

        [DataMember]
        public decimal? AnnualLeaveEntitilements { get; set; }

        [DataMember]
        public decimal? EarnedLeave { get; set; }

        [DataMember]
        public decimal? LossofPay { get; set; }

        [DataMember]
        public decimal? NoofDaysInTheMonth { get; set; }
        [DataMember]
        public decimal? NoofDaysInTheMonthLS { get; set; }

        [DataMember]
        public decimal? NoofLOPDays { get; set; }

        [DataMember]
        public decimal? MonthSalaryAmount { get; set; }

        [DataMember]
        public decimal? LeaveSalaryAmount { get; set; }

        [DataMember]
        public decimal? Gratuity { get; set; }

        [DataMember]
        public decimal? TotalAmount { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeSalaryStructure { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeSettlementType { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public long? SalaryStructureID { get; set; }
        [DataMember]
        public List<EmployeeSalaryStructureComponentMapDTO> SalaryComponents { get; set; }

        [DataMember]
        public long? LeaveSalaryStructureID { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeLeaveSalaryStructure { get; set; }

        [DataMember]
        public List<EmployeeSalaryStructureComponentMapDTO> LeaveSalaryComponents { get; set; }

        [DataMember]
        public List<SalarySlipDTO> SalarySlipDTOs { get; set; }

        [DataMember]
        public bool IsRegenerate { get; set; } = false;

        [DataMember]
        public bool IsFromSalarySlipGeneration { get; set; } = false;

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public bool? IsVacationSalary { get; set; } = false;

        [DataMember]
        public decimal? VacationDaysInPrevMonth { get; set; }

        [DataMember]
        public decimal? VacationDaysInCurrentMonth { get; set; }

        [DataMember]
        public decimal? NoofDaysInPrevMonth { get; set; }
        [DataMember]
        public int? VacationSalaryComponent { get; set; }
        [DataMember]
        public bool IsSecondMonthVacation { get; set; } = false;

    }
}

