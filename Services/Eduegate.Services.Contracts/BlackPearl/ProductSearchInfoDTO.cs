using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class ProductSearchInfoDTO
    {
        [DataMember]
        public string FreeText { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public decimal CategoryID { get; set; }
        [DataMember]
        public string SortByOption { get; set; }
        [DataMember]
        public PaginationDTO PageDetails { get; set; }
    }
}
