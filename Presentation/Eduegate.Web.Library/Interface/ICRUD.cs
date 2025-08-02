using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.Interface
{
    public interface ICRUD
    {
        List<ClaimViewModel> Claims { get; set; }
        List<KeyValueViewModel> Parameters { get; set; }
        List<ClientParameter> ClientParameters { get; set; }
        List<ScreenFieldSettingViewModel> FieldSettings { get; set; }
    }
}
