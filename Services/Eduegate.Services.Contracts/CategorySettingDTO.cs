using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
 
    [DataContract]
   public class CategorySettingDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CategorySettingsID { get; set; }
        [DataMember]
        public Nullable<long> CategoryID { get; set; }
        [DataMember]
        public string SettingCode { get; set; }
        [DataMember]
        public string SettingValue { get; set; }
        [DataMember]
        public Nullable<byte> UIControlTypeID { get; set; }
        [DataMember]
        public Nullable<int> LookUpID { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
