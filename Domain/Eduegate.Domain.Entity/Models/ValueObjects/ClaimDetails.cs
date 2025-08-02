using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class ClaimDetails
    {
        [Key]
        public long ClaimIID { get; set; }
        public string ClaimName { get; set; }
        public string ResourceName { get; set; }
        public Nullable<int> ClaimTypeID { get; set; }
        public string ClaimTypeName { get; set; }
    }
}
