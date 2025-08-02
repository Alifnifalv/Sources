using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    public class PaginationDTO
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
