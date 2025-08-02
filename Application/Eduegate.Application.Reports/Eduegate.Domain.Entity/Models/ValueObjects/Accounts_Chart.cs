using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class Accounts_Chart
    {
        public string PartCode { get; set; }
        public string Particulars { get; set; }
        public int Level { get; set; }
        public string GL { get; set; }
        public string Level_Sort { get; set; }
        public long Group_ID { get; set; }
        public int Parent_ID { get; set; }
    }
}
