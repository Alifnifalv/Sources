using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CheckAvailablity
    {
        [Key]
        public long CheckAvailableID { get; set; }
        public int ProductID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationCountry { get; set; }
        public Nullable<System.DateTime> CreatedDatetimeStamp { get; set; }
    }
}
