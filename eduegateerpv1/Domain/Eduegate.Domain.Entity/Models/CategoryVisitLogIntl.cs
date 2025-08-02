using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryVisitLogIntl
    {
        [Key]
        public int LogID { get; set; }
        public int CategoryID { get; set; }
        public System.DateTime VisitOn { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public short RefCountryID { get; set; }
    }
}
