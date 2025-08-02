using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FiscalYear_20210413
    {
        public int FiscalYear_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FiscalYear_Name { get; set; }
        public int? FiscalYear_Status { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? EndDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string CurrentYear { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsHide { get; set; }
    }
}
