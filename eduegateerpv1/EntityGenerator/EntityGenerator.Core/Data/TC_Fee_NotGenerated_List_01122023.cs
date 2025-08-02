using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TC_Fee_NotGenerated_List_01122023
    {
        [StringLength(50)]
        public string StudentStatus { get; set; }
        [StringLength(50)]
        public string TransferStatus { get; set; }
        public long StudentTransferRequestIID { get; set; }
        public byte? StudentStatusID { get; set; }
        public long StudentIID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public byte TransferRequestStatusID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public string SchoolRemarks { get; set; }
    }
}
