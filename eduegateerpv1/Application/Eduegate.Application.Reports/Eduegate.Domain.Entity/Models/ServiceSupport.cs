using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceSupport
    {
        public int TicketID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Subject { get; set; }
        public Nullable<long> OrderNo { get; set; }
        public Nullable<int> RefJobCardMasterID { get; set; }
        public string Comments { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationCountry { get; set; }
        public System.DateTime submitdate { get; set; }
        public bool isRead { get; set; }
        public string TicketStatus { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<short> RefUserID { get; set; }
        public Nullable<short> CreatedUserID { get; set; }
    }
}
