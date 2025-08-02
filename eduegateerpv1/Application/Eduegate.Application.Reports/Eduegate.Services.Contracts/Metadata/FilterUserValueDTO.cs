using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Metadata
{
    public class FilterUserValueDTO
    {
        public SearchView ViewID { get; set; }
        public long? LoginID { get; set; }
        public long FilterColumnID { get; set; }
        public Enums.Conditions Condition { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public int CompanyID { get; set; }
    }
}
