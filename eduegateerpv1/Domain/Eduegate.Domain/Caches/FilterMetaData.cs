using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Metadata;

namespace Eduegate.Domain.Caches
{
    public class FilterMetaData : IRepositoryItem
    {
        public List<FilterColumnDTO> FilterColumns { get; set; }
        public Eduegate.Services.Contracts.Enums.SearchView View { get; set; }

        public string ItemId
        {
            get { return View.ToString(); }
        }
    }
}
