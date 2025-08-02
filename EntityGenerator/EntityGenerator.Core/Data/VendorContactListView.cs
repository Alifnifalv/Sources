using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VendorContactListView
    {
        public long ContactIID { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        [StringLength(50)]
        public string SupplierCode { get; set; }
        public string Title { get; set; }
        [StringLength(50)]
        public string PhoneNo { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        public int? CreatedBy { get; set; }
        public long? SupplierID { get; set; }
        public long? LoginID { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsPrimaryContactPerson { get; set; }
    }
}
