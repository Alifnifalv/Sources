using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoWarehouseMaster
    {
        public IntlPoWarehouseMaster()
        {
            this.IntlPoGrnMasters = new List<IntlPoGrnMaster>();
        }

        [Key]
        public byte IntlPoWarehouseMasterID { get; set; }
        public string IntlPoWarehouseCode { get; set; }
        public string IntlPoWarehouseName { get; set; }
        public bool IntlPoWarehouseActive { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual ICollection<IntlPoGrnMaster> IntlPoGrnMasters { get; set; }
    }
}
