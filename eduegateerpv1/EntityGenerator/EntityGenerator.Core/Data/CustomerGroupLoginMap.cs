using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerGroupLoginMaps", Schema = "marketing")]
    public partial class CustomerGroupLoginMap
    {
        [Key]
        public long CustomerGroupLoginMapIID { get; set; }
        public long? CustomerGroupID { get; set; }
        public long? LoginID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
