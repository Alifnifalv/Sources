using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mutual
{
    [Table("schools.SchoolGeoMaps")]
    public partial class SchoolGeoMap
    {
        [Key]
        public long SchoolGeoMapIID { get; set; }

        public int? SchoolID { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public long? AreaID { get; set; }
    }
}
