using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PackingTypes", Schema = "catalog")]
    public partial class PackingType
    {
        [Key]
        public short PackingTypeIID { get; set; }
        public string PackingType1 { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> PackingCost { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
    }
}
