using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.CachItems
{
    public class LanguageCaches : IRepositoryItem
    {
        public LanguageCaches()
        {

        }

        public LanguageCaches(string cacheID)
        {
            CacheID = cacheID;
        }

        public List<LanguageDTO> Languages { get; set; }
        public string CacheID { get; set; }

        public string ItemId
        {
            get { return string.IsNullOrEmpty(CacheID) ? "Languages": CacheID; }
        }
    }
}
