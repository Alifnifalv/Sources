using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeSalaryStructureDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public EmployeeSalaryStructureDTO()
        {
            TimeSheetSalaryComponent = new KeyValueDTO();
            SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();
            LeaveSalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();
            PayrollFrequency = new KeyValueDTO();
            PaymentMode = new KeyValueDTO();
            Account = new KeyValueDTO();
            Employee = new List<KeyValueDTO>();
            EmployeeSalaryStructure = new KeyValueDTO();
        }
        [DataMember]
        public long EmployeeSalaryStructureIID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long? SalaryStructureID { get; set; }

        [DataMember]
        public System.DateTime? FromDate { get; set; }

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
        public long? AccountID { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeSalaryStructure { get; set; }

        [DataMember]
        public KeyValueDTO PayrollFrequency { get; set; }

        [DataMember]
        public KeyValueDTO TimeSheetSalaryComponent { get; set; }

        [DataMember]
        public KeyValueDTO PaymentMode { get; set; }

        [DataMember]
        public KeyValueDTO Account { get; set; }
        
        [DataMember]
        public List<KeyValueDTO> Employee { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public List<EmployeeSalaryStructureComponentMapDTO> SalaryComponents { get; set; }

        [DataMember]
        public long? LeaveSalaryStructureID { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeLeaveSalaryStructure { get; set; }

        [DataMember]
        public List<EmployeeSalaryStructureComponentMapDTO> LeaveSalaryComponents { get; set; }
    }
}
