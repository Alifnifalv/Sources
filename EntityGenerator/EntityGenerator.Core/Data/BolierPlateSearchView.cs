using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BolierPlateSearchView
    {
        public long BoilerPlateID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Template { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
    }
}
