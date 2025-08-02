using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AreaSearchView
    {
        public int AreaID { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        public short? ZoneID { get; set; }
        public int? CityID { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(9)]
        [Unicode(false)]
        public string StatusName { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public int? RouteID { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? CountryID { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
        public string RouteName { get; set; }
        [StringLength(50)]
        public string ZoneName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
    }
}
