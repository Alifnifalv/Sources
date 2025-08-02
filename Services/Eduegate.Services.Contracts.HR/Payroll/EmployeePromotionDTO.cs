using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeePromotionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeePromotionIID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long? SalaryStructureID { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public byte? PayrollFrequencyID { get; set; }

        [DataMember]
        public bool? IsSalaryBasedOnTimeSheet { get; set; }

        [DataMember]
        public int? TimeSheetSalaryComponentID { get; set; }

        [DataMember]
        public decimal? TimeSheetHourRate { get; set; }

        [DataMember]
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }

        [DataMember]
        public decimal? TimeSheetMaximumBenefits { get; set; }

        [DataMember]
        public int? PaymentModeID { get; set; }

        [DataMember]
        public int? OldLeaveGroupID { get; set; }

        [DataMember]
        public int? NewLeaveGroupID { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public int? OldRoleID { get; set; }

        [DataMember]
        public int? OldDesignationID { get; set; }

        [DataMember]
        public long? OldBranchID { get; set; }

        [DataMember]
        public int? NewRoleID { get; set; }

        [DataMember]
        public int? NewDesignationID { get; set; }

        [DataMember]
        public long? NewBranchID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeSalaryStructure { get; set; }

        [DataMember]
        public KeyValueDTO OldBranch { get; set; }

        [DataMember]
        public KeyValueDTO OldDesignation { get; set; }


        [DataMember]
        public KeyValueDTO NewBranch { get; set; }

        [DataMember]
        public KeyValueDTO NewDesignation { get; set; }

        [DataMember]
        public KeyValueDTO OldLeaveGroup { get; set; }

        [DataMember]
        public KeyValueDTO NewLeaveGroup { get; set; }

        [DataMember]
        public KeyValueDTO PaymentMode { get; set; }

        [DataMember]
        public KeyValueDTO PayrollFrequency { get; set; }

        [DataMember]
        public EmployeeSalaryStructureDTO SalaryStructure { get; set; }

        [DataMember]
        public KeyValueDTO TimeSheetSalaryComponent { get; set; }

        [DataMember]
        public KeyValueDTO Account { get; set; }

        [DataMember]
        public bool? IsApplyImmediately { get; set; }

        [DataMember]
        public List<EmployeePromotionComponentMapDTO> SalaryComponents { get; set; }

        [DataMember]
        public List<EmployeePromotionLeaveAllocDTO> EmployeePromotionLeaveAllocs { get; set; }

    }
   
}
