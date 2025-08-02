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
    public class SalaryStructureDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SalaryStructureDTO()
        {
            TimeSheetSalaryComponent = new KeyValueDTO();
            SalaryComponents = new List<SalaryStructureComponentDTO>();
            SalaryStructureScale = new List<SalaryStructureScaleDTO>();
            PayrollFrequency = new KeyValueDTO();
            PaymentMode = new KeyValueDTO();
            Account = new KeyValueDTO();         
        }

        [DataMember]
        public long SalaryStructureID { get; set; }
        [DataMember]
        public string StructureName { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }
        [DataMember]
        public byte? PayrollFrequencyID { get; set; }
        [DataMember]
        public KeyValueDTO PayrollFrequency { get; set; }
        [DataMember]
        public bool? IsSalaryBasedOnTimeSheet { get; set; }
        [DataMember]
        public int? TimeSheetSalaryComponentID { get; set; }
        [DataMember]
        public KeyValueDTO TimeSheetSalaryComponent { get; set; }
        [DataMember]
        public decimal? TimeSheetHourRate { get; set; }
        [DataMember]
        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }
        [DataMember]
        public decimal? TimeSheetMaximumBenefits { get; set; }
        [DataMember]
        public int? PaymentModeID { get; set; }
        [DataMember]
        public KeyValueDTO PaymentMode { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public KeyValueDTO Account { get; set; }

        [DataMember]
        public string StructureCode { get; set; }

        [DataMember]
        public List<SalaryStructureComponentDTO> SalaryComponents { get; set; }

        [DataMember]
        public List<SalaryStructureScaleDTO> SalaryStructureScale { get; set; }
    }
}
