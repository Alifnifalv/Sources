using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
   public class CategoryCultureDatas
    {
        public long CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryKeyWords { get; set; }
        public bool IsActive { get; set; }
    }
}
