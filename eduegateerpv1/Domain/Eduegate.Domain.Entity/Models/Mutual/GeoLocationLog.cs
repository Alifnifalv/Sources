using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mutual
{
    [Table("GeoLocationLogs", Schema = "mutual")]
    public class GeoLocationLog
    {
        [Key]
        public long GeoLocationLogIID { get; set; }
        public string Type { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ReferenceID1 { get; set; }
        public string ReferenceID2 { get; set; }
        public string ReferenceID3 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
