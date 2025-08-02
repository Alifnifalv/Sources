using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobApplication
    {
        public int ApplicationID { get; set; }
        public Nullable<int> JobID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string CV { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
    }
}
