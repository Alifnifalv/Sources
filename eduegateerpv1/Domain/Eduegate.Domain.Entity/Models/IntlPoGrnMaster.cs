using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoGrnMaster
    {
        public IntlPoGrnMaster()
        {
            this.IntlPoGrnDetails = new List<IntlPoGrnDetail>();
            this.IntlPoGrnLogs = new List<IntlPoGrnLog>();
        }

        [Key]
        public int IntlPoGrnMasterID { get; set; }
        public byte RefIntlPoWarehouseMasterID { get; set; }
        public Nullable<System.DateTime> GrnDate { get; set; }
        public string GrnStatus { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<IntlPoGrnDetail> IntlPoGrnDetails { get; set; }
        public virtual ICollection<IntlPoGrnLog> IntlPoGrnLogs { get; set; }
        public virtual IntlPoWarehouseMaster IntlPoWarehouseMaster { get; set; }
    }
}
