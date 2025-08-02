using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Accounts
{
    [DataContract]
    public class AdditionalExpensDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AdditionalExpensDTO()
        {
        }

        [DataMember]
        public int AdditionalExpenseID { get; set; }

        [DataMember]
        public string AdditionalExpenseCode { get; set; }

        [DataMember]
        public string AdditionalExpenseName { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public byte? CompanyID { get; set; }
    }
    
}
