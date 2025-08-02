using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class DashBoardChartDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO 
    {

        public DashBoardChartDTO()
        {

        }

        [DataMember]
        public List<string> ColumnDatas { get; set; }

        [DataMember]
        public List<string> ColumnHeaders { get; set; }

        [DataMember]
        public List<string> ColumnRelations { get; set; }        

    }   
}
