using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AreaMaster
    {
        [Key]
        public long AreaID { get; set; }
        public long RefCountryID { get; set; }
        public string AreaNameEn { get; set; }
        public string AreaNameAr { get; set; }
        public byte RouteID { get; set; }
        public bool Active { get; set; }
    }
}
