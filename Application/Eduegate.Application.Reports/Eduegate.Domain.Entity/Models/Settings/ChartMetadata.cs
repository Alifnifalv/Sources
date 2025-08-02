using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Settings
{
    [Table("setting.ChartMetadatas")]
    public partial class ChartMetadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChartMetadataID { get; set; }

        [StringLength(100)]
        public string ChartName { get; set; }

        [StringLength(5)]
        public string ChartType { get; set; }

        [StringLength(200)]
        public string ChartPhysicalEntiy { get; set; }
    }
}
