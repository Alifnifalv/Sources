using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeCollectionStatuses", Schema = "schools")]
    public partial class FeeCollectionStatus
    {
        public FeeCollectionStatus()
        {
            FeeCollections = new HashSet<FeeCollection>();
        }

        [Key]
        public int FeeCollectionStatusID { get; set; }
        [StringLength(250)]
        public string StatusNameEn { get; set; }
        [StringLength(250)]
        public string StatusNameAr { get; set; }
        [StringLength(100)]
        public string StatusTitleEn { get; set; }
        [StringLength(100)]
        public string StatusTitleAr { get; set; }

        [InverseProperty("FeeCollectionStatus")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
    }
}
