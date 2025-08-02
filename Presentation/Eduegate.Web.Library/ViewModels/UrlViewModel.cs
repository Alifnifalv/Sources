using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class UrlViewModel : BaseMasterViewModel
    {
        public UrlViewModel()
        {
            IsOnInit = true;
        }

        public bool IsOnInit { get;set; }
        public string LookUpName { get; set; }
        public string Url { get; set; }
        public string CallBack { get; set; }
    }
}