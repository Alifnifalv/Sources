using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AreaDeliverySetting
    {
        public int AreaID { get; set; }
        public short? ZoneID { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        public string RouteName { get; set; }
    }
}
