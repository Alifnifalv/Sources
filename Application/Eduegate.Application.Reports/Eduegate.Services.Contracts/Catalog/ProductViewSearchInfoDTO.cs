using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductViewSearchInfoDTO
    {
        [DataMember]
        public string SortByOption { get; set; }
        [DataMember]
        public PaginationDTO PageDetails { get; set; }
    }
}
