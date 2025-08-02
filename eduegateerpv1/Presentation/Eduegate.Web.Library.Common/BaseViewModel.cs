using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class BaseViewModel
    {
        public string CurrencyCode { get; set; }
        public string CurrencyDisplayText { get; set; }
    }
}