using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CustomEntity
{
    [Keyless]
    public class VWS_Fee_Outstanding_Current 
    {
        public string InvoiceNo { get; set; }

        public string InvoiceDate { get; set; }

        public DateTime TranDate { get; set; }

        public int? ClassID { get; set; }

        public string Class { get; set; }

        public long StudentIID { get; set; }

        public string AdmissionNumber { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcadamicYearID { get; set; }

        public string StudentName { get; set; }

        public string Section { get; set; }

        public long StudentFeeDueIID { get; set; }

        public long ParentID { get; set; }

        public string ParentCode { get; set; }

        public long? ParentLoginID { get; set; }

        public string ParentEmail { get; set; }

        public string FatherName { get; set; }

        public string PhoneNumber { get; set; }

        public int? FeeMasterID { get; set; }

        public string FeeName { get; set; }

        public byte FeeCycleID { get; set; }

        public string Fee_Due { get; set; }

        public decimal Fee_Col { get; set; }

        public decimal Fee_Crn { get; set; }

        public decimal Fee_Stl { get; set; }

        public decimal? Fee_Bal { get; set; }
    }
}