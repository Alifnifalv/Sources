using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("KnowHowOption", Schema = "cms")]
    public partial class KnowHowOption
    {
        public KnowHowOption()
        {
            //this.Customers = new List<Customer>();
        }

        [Key]
        public long KnowHowOptionIID { get; set; }
        public string KnowHowOptionText { get; set; }
        public bool IsEditable { get; set; }
        //public virtual ICollection<Customer> Customers { get; set; }
    }
}
