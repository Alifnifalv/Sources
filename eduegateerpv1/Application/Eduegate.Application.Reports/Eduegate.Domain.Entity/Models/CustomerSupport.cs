using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerSupport
    {
        public int TicketID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Subject { get; set; }
        public Nullable<long> OrderNo { get; set; }
        public string Comments { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationCountry { get; set; }
        public System.DateTime submitdt { get; set; }
        public bool isRead { get; set; }
        public string TicketStatus { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public Nullable<long> CreatedUserID { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
