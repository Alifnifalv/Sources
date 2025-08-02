using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierSearchView1
    {
        public long SupplierIID { get; set; }
        [Required]
        [StringLength(152)]
        public string Supplier { get; set; }
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [StringLength(255)]
        public string VendorNickName { get; set; }
        [StringLength(767)]
        public string ContactPerson { get; set; }
        [StringLength(50)]
        public string MobileNo1 { get; set; }
        [StringLength(399)]
        public string AddressName { get; set; }
        public long? branchiid { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string branchname { get; set; }
        public string ProductManager { get; set; }
        public string Entitlements { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public int? companyID { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
        [StringLength(100)]
        public string SupplierEmail { get; set; }
        [StringLength(50)]
        public string VendorCR { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
