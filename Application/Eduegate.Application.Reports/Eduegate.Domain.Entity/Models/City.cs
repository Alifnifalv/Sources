using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class City
    {
        public City()
        {
            this.Areas = new List<Area>();
            this.Vehicles = new List<Vehicle>();
        }

        public int CityID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CityName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
