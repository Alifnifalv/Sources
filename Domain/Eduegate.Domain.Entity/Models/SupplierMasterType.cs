using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierMasterType
    {
        [Key]
        public byte SupplierMasterTypeID { get; set; }
        public string SupplierText { get; set; }
        public string SupplierType { get; set; }
    }
}
