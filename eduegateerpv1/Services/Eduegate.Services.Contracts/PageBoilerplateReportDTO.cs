using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contract

{
    [DataContract]
    public class PageBoilerplateReportDTO
    {
        [DataMember]
        public long PageBoilerplateReportID { get; set; }
        [DataMember]
        public long? BoilerPlateID { get; set; }
        [DataMember]
        public long? PageID { get; set; }
        [DataMember]
        public string ReportName { get; set; }
        [DataMember]
        public string ReportHeader { get; set; }
        [DataMember]
        public string ParameterName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
      
    }
}
