using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ServiceProviderLogsSearchView
    {
        [StringLength(20)]
        public string ProviderCode { get; set; }
        [StringLength(50)]
        public string ProviderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LogDateTime { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public long? ReferenceID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
    }
}
