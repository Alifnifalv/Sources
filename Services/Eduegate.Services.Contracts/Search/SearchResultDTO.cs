using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts.Search
{

    [DataContract]
    public class SearchResultDTO
    {
        public SearchResultDTO()
        {
            Columns = new List<ColumnDTO>();
            Rows = new List<DataRowDTO>();
        }

        [DataMember]
        public List<ColumnDTO> Columns { get; set; }
        [DataMember]
        public List<DataRowDTO> Rows { get; set; }
        [DataMember]
        public long TotalRecords { get; set; }
        [DataMember]
        public long CurrentPage { get; set; }
        [DataMember]
        public long PageSize { get; set; }

        [DataMember]
        public decimal TotalSalaries { get; set; }
    }
}
