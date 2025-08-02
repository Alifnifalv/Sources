using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Budgeting
{
    [DataContract]
    public class BudgetMasterDTO : BaseMasterDTO
    {
        public BudgetMasterDTO()
        {
            Currency = new KeyValueDTO();
            Department = new KeyValueDTO();
        }

        [DataMember]
        public int BudgetID { get; set; }

        [DataMember]
        public string BudgetCode { get; set; }

        [DataMember]
        public string BudgetName { get; set; }

        [DataMember]
        public DateTime? PeriodStart { get; set; }

        [DataMember]
        public DateTime? PeriodEnd { get; set; }

        [DataMember]
        public byte? BudgetStatusID { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        [DataMember]
        public int? CurrencyID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public decimal? BudgetTotalValue { get; set; }

        [DataMember]
        public string BudgetStatus { get; set; }

        [DataMember]
        public byte? BudgetTypeID { get; set; }

        [DataMember]
        public byte? BudgetGroupID { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public int? FinancialYearID { get; set; }

        [DataMember]
        public string BudgetType { get; set; }

        [DataMember]
        public string BudgetGroup { get; set; }

        [DataMember]
        public KeyValueDTO Currency { get; set; }

        [DataMember]
        public KeyValueDTO Department { get; set; }

        [DataMember]
        public string PeriodStartDateString { get; set; }

        [DataMember]
        public string PeriodEndDateString { get; set; }

    }
}