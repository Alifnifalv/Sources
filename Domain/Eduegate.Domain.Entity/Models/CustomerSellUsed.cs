using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerSellUsed
    {
        [Key]
        public int TicketID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ProductDetails { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Remarks { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationCountry { get; set; }
        public System.DateTime submitdt { get; set; }
        public bool isRead { get; set; }
        public string Status { get; set; }
        public Nullable<long> StatusBy { get; set; }
        public Nullable<System.DateTime> StatusDt { get; set; }
    }
}
