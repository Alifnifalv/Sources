using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LibraryBookDTO()
        {
            BookCategories = new List<KeyValueDTO>();
            Subjects = new KeyValueDTO();
            LibraryBookCategoryCode = new KeyValueDTO();
            LibraryBookMaps = new List<LibraryBookMapDTO>();
        }

        [DataMember]
        public long  LibraryBookIID { get; set; }
        [DataMember]
        public string  BookTitle { get; set; }
        [DataMember]
        public string  BookNumber { get; set; }
        [DataMember]
        public string  ISBNNumber { get; set; }
        [DataMember]
        public string  Publisher { get; set; }
        [DataMember]
        public string  Author { get; set; }
        [DataMember]
        public string  Subject { get; set; }
        [DataMember]
        public string  RackNumber { get; set; }
        [DataMember]
        public int?  Quatity { get; set; }
        [DataMember]
        public decimal?  BookPrice { get; set; }
        [DataMember]
        public decimal?  PostPrice { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public byte? BookTypeID { get; set; }
        [DataMember]
        public byte? BookConditionID { get; set; }
        [DataMember]
        public byte? BookStatusID { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? BookCategoryCodeID { get; set; }
        [DataMember]
        public KeyValueDTO LibraryBookCategoryCode { get; set; }
        [DataMember]
        public string BookCodeToDto { get; set; }
        [DataMember]
        public string BookCode { get; set; }
        [DataMember]
        public string Series { get; set; }
        [DataMember]
        public string Pages { get; set; }
        [DataMember]
        public string Acc_No { get; set; }
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public string Edition { get; set; }
        [DataMember]
        public string Call_No { get; set; }
        [DataMember]
        public string Bill_No { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public List<KeyValueDTO> BookCategories { get; set; }

        [DataMember]
        public KeyValueDTO Subjects { get; set; }

        [DataMember]
        public string BookCategoryName { get; set; }
        [DataMember]
        public int? BookCodeSequenceNo { get; set; }

        [DataMember]
        public string BookDetails { get; set; }

        [DataMember]
        public string PlaceOfPublication { get; set; }

        [DataMember]
        public List<LibraryBookMapDTO> LibraryBookMaps { get; set; }
    }
}


