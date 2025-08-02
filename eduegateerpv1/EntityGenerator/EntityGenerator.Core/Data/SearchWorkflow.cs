using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SearchWorkflow
    {
        public long WorkflowIID { get; set; }
        [StringLength(100)]
        public string WokflowName { get; set; }
        public int? WorkflowTypeID { get; set; }
        [StringLength(50)]
        public string WorkflowTypeName { get; set; }
        public int? LinkedEntityTypeID { get; set; }
        public int? WorkflowApplyFieldID { get; set; }
        [StringLength(100)]
        public string ColumnName { get; set; }
    }
}
