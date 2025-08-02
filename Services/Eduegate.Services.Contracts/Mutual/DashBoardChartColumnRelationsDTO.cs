using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class  DashBoardChartColumnRelationsDTO
    {
        public DashBoardChartColumnRelationsDTO()
        {

        }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Destination { get; set; }
    }
}
