using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class SchoolCreditNoteDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SchoolCreditNoteDTO()
        {
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Student = new KeyValueDTO();
            CreditNoteFeeTypeMapDTO = new List<CreditNoteFeeTypeDTO>();
        }

        [DataMember]
        public long SchoolCreditNoteIID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }       
        [DataMember]
        public DateTime? CreditNoteDate { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public KeyValueDTO Class { get; set; }
        [DataMember]
        public List<CreditNoteFeeTypeDTO> CreditNoteFeeTypeMapDTO { get; set; }
        [DataMember] 
        public KeyValueDTO Student { get; set; }
        //public List<KeyValueDTO> Student { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public bool? IsDebitNote { get; set; }

        [DataMember]
        public string CreditNoteNumber { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }


    }
}
