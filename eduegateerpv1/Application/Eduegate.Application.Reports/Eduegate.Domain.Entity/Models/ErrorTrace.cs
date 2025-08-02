using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ErrorTrace
    {
        public int id { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public string ERROR_LINE { get; set; }
        public string SP_Name { get; set; }
        public System.DateTime ErrorOn { get; set; }
    }
}
