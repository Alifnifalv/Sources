using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Payment
{
    public class QPAYResponseViewModel : BaseMasterViewModel
    {
        public string Status { get; set; }
       
        public string ErrorMessage { get; set; }
        
        public string StackTrace { get; set; }
     
        public string InnerErrorMessage { get; set; }
    }
}
