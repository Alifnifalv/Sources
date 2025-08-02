using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerContactsSearchView
    {
        public long CustomerIID { get; set; }
        public long ContactIID { get; set; }
        public string CustomerCR { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string AddressName { get; set; }
        public string PostalCode { get; set; }
        public string MobileNo1 { get; set; }
        public string CivilIDNumber { get; set; }
    }
}
