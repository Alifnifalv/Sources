using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class ReWriteUrlType
    {
        public long IID { get; set; }
        public string Code { get; set; }
        public int UrlType { get; set; }
        public string Url { get; set; }
        public int LevelNo { get; set; }
    }
}
