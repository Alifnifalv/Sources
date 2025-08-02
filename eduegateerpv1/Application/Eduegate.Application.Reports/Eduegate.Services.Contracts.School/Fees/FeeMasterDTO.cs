using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeMasterDTO()
        {
            LedgerAccount = new KeyValueDTO();
            TaxLedgerAccount = new KeyValueDTO();
            AdvanceAccount = new KeyValueDTO();
            AdvanceTaxAccount = new KeyValueDTO();
            OSTaxAccount = new KeyValueDTO();
            OutstandingAccount = new KeyValueDTO();           
            ProvisionforAdvanceAccount = new KeyValueDTO();
            ProvisionforOutstandingAccount = new KeyValueDTO();
        }

        [DataMember]
        public int  FeeMasterID { get; set; }
       
        //[DataMember]
        //public int?  FeeGroupID { get; set; }
        //[DataMember]
        //public string FeeGroup { get; set; }
        [DataMember]
        public int?  FeeTypeID { get; set; }

        [DataMember]
        public string FeeType { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public System.DateTime?  DueDate { get; set; }
        [DataMember]
        public decimal?  Amount { get; set; }
        [DataMember]
        public byte? FeeCycleID { get; set; }
        [DataMember]
        public string FeeCycle { get; set; }
        //[DataMember]
        //public byte? IsInstallment { get; set; }
        [DataMember]
        public long? LedgerAccountID { get; set; }
        //[DataMember]
        //public string LedgerAccount { get; set; }
        [DataMember]
        public long? TaxLedgerAccountID { get; set; }

        //[DataMember]
        //public string TaxLedgerAccount { get; set; }
        [DataMember]
        public decimal? TaxPercentage { get; set; }
        [DataMember]
        public int? FeePeriodID { get; set; }
        //[DataMember]
        //public string FeePeriod { get; set; }

        [DataMember]
        public int? DueInDays { get; set; }
        
        [DataMember]
        public KeyValueDTO LedgerAccount { get; set; }
        [DataMember]
        public KeyValueDTO TaxLedgerAccount { get; set; }
        //[DataMember]
        //public KeyValueDTO TaxPercentage { get; set; }
        [DataMember]
        public long? AdvanceAccountID { get; set; }

        [DataMember]
        public long? AdvanceTaxAccountID { get; set; }

        [DataMember]
        public KeyValueDTO ProvisionforAdvanceAccount { get; set; }

        [DataMember]
        public KeyValueDTO ProvisionforOutstandingAccount { get; set; }

        [DataMember]
        public long? ProvisionforAdvanceAccountID { get; set; }

        [DataMember]
        public long? ProvisionforOutstandingAccountID { get; set; }

        [DataMember]
        public decimal? AdvanceTaxPercentage { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO AdvanceAccount { get; set; }
        [DataMember]
        public KeyValueDTO AdvanceTaxAccount { get; set; }

        [DataMember]
        public long? OSTaxAccountID { get; set; }
        [DataMember]
        public long? OutstandingAccountID { get; set; }
        [DataMember]
        public decimal? OSTaxPercentage { get; set; }

        [DataMember]
        public KeyValueDTO OSTaxAccount { get; set; }
        [DataMember]
        public KeyValueDTO OutstandingAccount { get; set; }

        [DataMember]
        public bool? IsExternal { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string ReportName { get; set; }

    }
}



