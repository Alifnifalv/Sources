using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("MarkRegisters_DatatobeEdited20230227", Schema = "schools")]
    public partial class MarkRegisters_DatatobeEdited20230227
    {
        public byte? schoolID { get; set; }
        public long? studentID { get; set; }
        public int? classID { get; set; }
        public int? oldSection { get; set; }
        public int? NewSection { get; set; }
    }
}
