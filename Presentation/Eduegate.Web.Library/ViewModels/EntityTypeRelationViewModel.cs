using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Web.Library.ViewModels
{
    public class EntityTypeRelationViewModel
    {
        public EntityTypes FromEntityTypes { get; set; }
        public EntityTypes ToEntityTypes { get; set; }
        public long FromRelaionID { get; set; }
        public List<long> ToRelaionIDs { get; set; }
    }
}
