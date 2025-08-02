using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;

namespace Eduegate.Web.Library.ViewModels.Common
{
    public class MenuCacheItemViewModel : IRepositoryItem
    {
        public List<Services.Contracts.MenuLinks.MenuDTO> Menus { get; set; }

        public string ItemId
        {
            get;
            set;
        }
    }
}
