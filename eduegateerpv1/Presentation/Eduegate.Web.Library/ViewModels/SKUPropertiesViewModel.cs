using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class SKUPropertiesViewModel
    {
        public decimal ProductFamilyIID { get; set; }
        public decimal ProductPropertyMapIID { get; set; }
        public decimal ProductPropertyID { get; set; }
        public string Value { get; set; }
    }
}