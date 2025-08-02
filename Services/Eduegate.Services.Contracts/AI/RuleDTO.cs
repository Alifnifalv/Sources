using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Services.Contracts.AI
{ 
    [DataContract]
    public  class RuleDTO
    {
        [DataMember]
        public long RuleID { get; set; }
        [DataMember]
        public int RuleSetID { get; set; }
        [DataMember]
        public int RuleTypeID { get; set; }
        [DataMember]
        public int? RuleOrder { get; set; }
        [DataMember]
        public string ColumnDataType { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public int PatternTypeID { get; set; }
        [DataMember]
        public string Pattern { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Expression { get; set; }
        [DataMember]
        public KeyValueDTO PatternType { get; set; }
        [DataMember]
        public KeyValueDTO RuleSet { get; set; }
        [DataMember]
        public KeyValueDTO RuleType { get; set; }
    }
}
