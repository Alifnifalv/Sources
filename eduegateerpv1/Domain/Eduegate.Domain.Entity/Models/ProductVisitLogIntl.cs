using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductVisitLogIntl
    {
        [Key]
        public int ProductVisitLogIntlID { get; set; }
        public int ProductID { get; set; }
        public System.DateTime VisitOn { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<bool> IsSearch { get; set; }
        public string VisitFrom { get; set; }
        public string VisitProduct { get; set; }
        public short RefCountryID { get; set; }
    }
}
