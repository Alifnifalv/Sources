using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class ProductSKUDetailsViewModel : BaseMasterViewModel
    {
        public long ProductSerialID { get; set; }
        public long DetailID { get; set; }
        public string SerialNo { get; set; }
        public string PartNo { get; set; }
        public int ProductLength { get; set; }
        public long ProductSKUMapID { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsFocus { get; set; }
    }
}
