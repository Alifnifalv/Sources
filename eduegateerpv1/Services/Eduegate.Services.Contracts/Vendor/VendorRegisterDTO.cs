using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Vendor
{
    public class VendorRegisterDTO
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string VendorCr { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string TelephoneNo { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string ConfirmPassword { get; set; }

        [DataMember]
        public string PasswordSalt { get; set; }

        [DataMember]
        public bool? IsError { get; set; }

        [DataMember]
        public string ReturnMessage { get; set; }

        [DataMember]
        public long? SupplierIID { get; set; }

        [DataMember]
        public string SupplierCode { get; set; }

        [DataMember]
        public long? LoginID { get; set; }
    }
}
