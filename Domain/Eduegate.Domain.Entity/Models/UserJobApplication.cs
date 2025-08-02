using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("UserJobApplications", Schema = "cms")]
    public partial class UserJobApplication
    {
        [Key]
        public long JobApplicationIID { get; set; }
        public long JobID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Resume { get; set; }
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
    }
}
