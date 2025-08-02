using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("GeoLocationLogs", Schema = "mutual")]
    public partial class GeoLocationLog
    {
        [Key]
        public long GeoLocationLogIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Type { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Latitude { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Longitude { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceID1 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceID2 { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceID3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
