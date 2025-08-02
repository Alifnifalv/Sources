using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PageBoilerplateMapParameterCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public long PageBoilerplateMapParameterID { get; set; }
        public string ParameterValue { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual PageBoilerplateMapParameter PageBoilerplateMapParameter { get; set; }
    }
}
