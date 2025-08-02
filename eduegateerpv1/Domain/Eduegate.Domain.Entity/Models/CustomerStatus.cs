using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CustomerStatuses", Schema = "mutual")]
    public partial class CustomerStatus
    {
        public CustomerStatus()
        {
            //this.Customers = new List<Customer>();
        }

        [Key]
        public byte CustomerStatusID { get; set; }
        public string StatusName { get; set; }
        //public virtual ICollection<Customer> Customers { get; set; }
    }
}
