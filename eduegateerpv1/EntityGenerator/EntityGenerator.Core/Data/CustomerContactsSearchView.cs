using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CustomerContactsSearchView
    {
        public long CustomerIID { get; set; }
        public long ContactIID { get; set; }
        [StringLength(50)]
        public string CustomerCR { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [StringLength(500)]
        public string AddressName { get; set; }
        [StringLength(100)]
        public string PostalCode { get; set; }
        [StringLength(50)]
        public string MobileNo1 { get; set; }
        [StringLength(100)]
        public string CivilIDNumber { get; set; }
    }
}
