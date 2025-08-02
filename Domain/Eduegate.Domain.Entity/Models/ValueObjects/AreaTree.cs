using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class AreaTree
    {
        public AreaTree()
        {
            Locations = new List<LocationTree>();
            AreaNodes = new List<AreaTree>();
        }

        [Key]
        public long AreaID { get; set; }
        public string AreaName { get; set; }
        public List<AreaTree> AreaNodes { get; set; }
        public List<LocationTree> Locations { get; set; }
    }
}
