using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CitySearchView
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
