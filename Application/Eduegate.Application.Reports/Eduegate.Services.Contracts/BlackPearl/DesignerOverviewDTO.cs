using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class DesignerOverviewDTO
    {
        [DataMember]
        public int DesignerId { get; set; }
        [DataMember]
        public string DesignerName { get; set; }
        [DataMember]
        public string DesignerDescription { get; set; }
        [DataMember]
        public string DesignerImagePath { get; set; }
    }
}
