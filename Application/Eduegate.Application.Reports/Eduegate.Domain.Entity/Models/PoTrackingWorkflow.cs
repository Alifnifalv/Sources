using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PoTrackingWorkflow
    {
        public byte PoTrackingWorkflowID { get; set; }
        public byte PoTrackingWorkflowNo { get; set; }
        public string ProcessName { get; set; }
        public byte ProcessSeq { get; set; }
    }
}
