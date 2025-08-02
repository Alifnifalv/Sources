using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CompanyStatus
    {
        public CompanyStatus()
        {
            this.Companies = new List<Company>();
        }

        public byte CompanyStatusID { get; set; } 
        public string StatusName { get; set; }
        public virtual ICollection<Company> Companies { get; set; } 
    }
}
