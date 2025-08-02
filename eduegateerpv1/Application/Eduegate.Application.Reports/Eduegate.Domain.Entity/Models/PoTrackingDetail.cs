using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PoTrackingDetail
    {
        public int PoTrackingDetailsID { get; set; }
        public int RefPoTrackingMasterID { get; set; }
        public byte RefPoTrackingWorkflowID { get; set; }
        public string Remarks { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public short UpdatedBy { get; set; }
    }
}
