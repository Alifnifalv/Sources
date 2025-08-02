using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Sites", Schema = "setting")]
    public partial class Site1
    {
        [Key]
        public long SiteID { get; set; }
        [StringLength(100)]
        public string SiteName { get; set; }
        public int? Created { get; set; }
        public int? Updated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
