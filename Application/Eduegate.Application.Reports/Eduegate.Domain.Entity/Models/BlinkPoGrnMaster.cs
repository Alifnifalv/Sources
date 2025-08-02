using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkPoGrnMaster
    {
        public BlinkPoGrnMaster()
        {
            this.BlinkPoGrnDetails = new List<BlinkPoGrnDetail>();
            this.BlinkPoGrnLogs = new List<BlinkPoGrnLog>();
        }

        public int BlinkPoGrnMasterID { get; set; }
        public short RefBlinkLocationMasterID { get; set; }
        public System.DateTime GrnDate { get; set; }
        public string GrnStatus { get; set; }
        public string Remarks { get; set; }
        public virtual BlinkLocationMaster BlinkLocationMaster { get; set; }
        public virtual ICollection<BlinkPoGrnDetail> BlinkPoGrnDetails { get; set; }
        public virtual ICollection<BlinkPoGrnLog> BlinkPoGrnLogs { get; set; }
    }
}
