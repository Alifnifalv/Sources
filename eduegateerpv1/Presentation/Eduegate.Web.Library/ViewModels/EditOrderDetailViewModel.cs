using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Web.Library.ViewModels
{
    public class EditOrderDetailViewModel
    {
        public ReplacementActions Action { get; set; }
        public int Quantity { get; set; }
    }
}
