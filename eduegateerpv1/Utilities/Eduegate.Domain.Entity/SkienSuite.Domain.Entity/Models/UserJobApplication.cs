using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserJobApplication
    {
        public long JobApplicationIID { get; set; }
        public long JobID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Resume { get; set; }
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
