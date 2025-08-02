using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class WPSDetailDTO
    {
        public WPSDetailDTO()
        {
            CsvFileHeaders = new List<KeyValueDTO>();
        }

        [DataMember]
        public long WPSIID { get; set; }

        [DataMember]
        public long? PayerBankDetailIID { get; set; }

        [DataMember]
        public string SalaryYear { get; set; }

        [DataMember]
        public int? SalaryMonth { get; set; }

        [DataMember]
        public decimal TotalSalaries { get; set; }

        [DataMember]
        public int? TotalRecords { get; set; }

        [DataMember]
        public long? ContentID { get; set; }

        [DataMember]
        public List<KeyValueDTO> CsvFileHeaders { get; set; }

        [DataMember]
        public string FileName { get; set; }
    }
}