using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryTransactionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public LibraryTransactionDTO()
        {
            Employee = new KeyValueDTO();
            Book = new KeyValueDTO();
            BookList = new List<KeyValueDTO>();
            Student = new KeyValueDTO();
        }
        [DataMember]
        public long  LibraryTransactionIID { get; set; }
        [DataMember]
        public byte?  LibraryTransactionTypeID { get; set; }
        [DataMember]
        public string  Notes { get; set; }
        [DataMember]
        public long?  BookID { get; set; }
        [DataMember]
        public long?  StudentID { get; set; }
        [DataMember]
        public long?  EmployeeID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string Acc_No { get; set; }

        [DataMember]
        public string Call_No { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public System.DateTime?  TransactionDate { get; set; }
        [DataMember]
        public System.DateTime? ReturnDueDate { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public List<KeyValueDTO> BookList { get; set; }

        [DataMember]
        public KeyValueDTO Book { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public byte? BookConditionID { get; set; }
        [DataMember]
        public string StaffName { get; set; }

        //[DataMember]
        //public bool? IsApplyDamageCost { get; set; }

        //[DataMember]
        //public decimal? PercentageDamageCost { get; set; }

        //[DataMember]
        //public bool? IsApplyLateFee { get; set; }

        //[DataMember]
        //public decimal? LateFeeAmount { get; set; }

        [DataMember]
        public bool? IsCollected { get; set; }

        [DataMember]
        public string BookTitle { get; set; }

        [DataMember]
        public string BookIssueDetails { get; set; }

        [DataMember]
        public string TransactionDateString { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public int? BookQuantity { get; set; }
        [DataMember]
        public string AvailableBookQty { get; set; }

        [DataMember]
        public int? LibraryBookMapID { get; set; }

    }
}