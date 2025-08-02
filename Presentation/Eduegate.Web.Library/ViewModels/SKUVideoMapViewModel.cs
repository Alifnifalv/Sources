using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class SKUVideoMapViewModel
    {
        public string VideoName { get; set; }
        public string VideoPath { get; set; }
        public long VideoMapID { get; set; }
        public long ProductVideoMapID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<byte> Sequence { get; set; }
    }
}
