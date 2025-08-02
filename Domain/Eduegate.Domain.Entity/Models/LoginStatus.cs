using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public class LoginStatus
    {
        [Key]
        public byte LoginStatusID { get; set; }
        public string Description { get; set; }
    }
}
