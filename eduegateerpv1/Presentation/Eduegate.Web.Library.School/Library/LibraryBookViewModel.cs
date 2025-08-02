using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Library
{
    public class LibraryBookViewModel : BaseMasterViewModel
    {
        public LibraryBookViewModel()
        {
            //BookCategories = new List<KeyValueViewModel>();
            IsActive = true;
        }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Id")]
        public long  LibraryBookIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("BookCategoryCode")]
        [Select2("LibraryBookCategoryCode", "Numeric", false, "GetBooKCategoryName($event, $element, CRUDModel.ViewModel)"/*, optionalAttribute1: "ng-disabled = 'CRUDModel.ViewModel.LibraryBookIID != 0'"*/)]
        [LookUp("LookUps.LibraryBookCategoryCode")]
        public KeyValueViewModel LibraryBookCategoryCode { get; set; }
        public long? BookCategoryCodeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("BookCode")]
        public string BookCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("BookCategory")]
        public string BookCategoryName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Booktitle")]
        public string  BookTitle { get; set; }

        //[Required]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Booknumber")]
        //public string  BookNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Booktype")]
        [LookUp("LookUps.BookType")]
        public string BookType { get; set; }
        public byte? BookTypeID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Subject")]
        public string Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Author")]
        public string Author { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Publisher")]
        public string Publisher { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Place of publication")]
        public string PlaceOfPublication { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ISBNnumber")]
        public string ISBNNumber { get; set; }

        //[Required]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ShelfNumber")]
        public string RackNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Bookcondition")]
        [LookUp("LookUps.BookTypeCondition")]
        public string BookCondition { get; set; }

        public byte? BookConditionID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        //[Select2("SubjectType", "String", true)]
        //[DisplayName("Book categories")]
        //[LookUp("LookUps.BookCategory")]
        //public List<KeyValueViewModel> BookCategories { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("CallNo")]
        public string Call_No { get; set; }

        //[Required]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = 'CRUDModel.ViewModel.LibraryBookIID != 0'")]
        [RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("Quantity")]
        public int? Quatity { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textleft")]
        //[CustomDisplay("")]
        //public string space { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea, attribs: "ng-disabled=CRUDModel.ViewModel")]
        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "labelinfo-custom")]
        [CustomDisplay("AccNo")]
        public string BookDetails { get; set; }

        //[Required]
        //[MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        ////[RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        //[CustomDisplay("AccNo")]
        public string Acc_No { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("Series")]
        public string Series { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("Pages")]
        public string Pages { get; set; }


        [Required]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("Year")]
        public string Year { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [CustomDisplay("Edition")]
        public string Edition { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[CustomDisplay("Bookstatus")]
        //[LookUp("LookUps.BookStatus")]
        //public string BookStatus { get; set; }
        //public byte? BookStatusID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine11 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textleft")]
        [CustomDisplay("BillNo")]
        public string Bill_No { get; set; }

        [Required]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("Bookprice")]
        public decimal? BookPrice { get; set; }
        
        //[Required]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        [RegularExpression(@"^[0-9*#+.]+$", ErrorMessage = "Use digits only")]
        [CustomDisplay("Postprice")]
        public decimal?  PostPrice { get; set; }
        public int? BookCodeSequenceNo { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryBookDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryBookDTO, LibraryBookViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var valueDTO = dto as LibraryBookDTO;
            var vm = Mapper<LibraryBookDTO, LibraryBookViewModel>.Map(valueDTO);
            vm.BookCode = valueDTO.BookCode;
            vm.BookCodeSequenceNo = valueDTO.BookCodeSequenceNo;
            vm.BookType = valueDTO.BookTypeID.HasValue ? valueDTO.BookTypeID.Value.ToString() : null;
            vm.BookCondition = valueDTO.BookConditionID.HasValue ? valueDTO.BookConditionID.Value.ToString() : null;
            //vm.BookStatus = valueDTO.BookStatusID.HasValue ? valueDTO.BookStatusID.Value.ToString() : null;
            vm.BookCategoryName = valueDTO.BookCategoryName;
            vm.BookDetails = valueDTO.BookDetails;
            vm.PlaceOfPublication = valueDTO.PlaceOfPublication;
            vm.LibraryBookCategoryCode = valueDTO.BookCategoryCodeID.HasValue ? new KeyValueViewModel()
            {
                Key = valueDTO.BookCategoryCodeID.ToString(),
                Value = valueDTO.LibraryBookCategoryCode.Value
            } : new KeyValueViewModel();


            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryBookViewModel, LibraryBookDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<LibraryBookViewModel, LibraryBookDTO>.Map(this);
            dto.BookTypeID = string.IsNullOrEmpty(this.BookType) ? (byte?)null : byte.Parse(this.BookType);
            dto.BookConditionID = string.IsNullOrEmpty(this.BookCondition) ? (byte?)null : byte.Parse(this.BookCondition);
            //dto.BookStatusID = string.IsNullOrEmpty(this.BookStatus) ? (byte?)null : byte.Parse(this.BookStatus);
            dto.BookCategoryCodeID = this == null || this.LibraryBookCategoryCode == null || string.IsNullOrEmpty(this.LibraryBookCategoryCode.Key) ? (long?)null : long.Parse(this.LibraryBookCategoryCode.Key);
            dto.BookCodeToDto = this.LibraryBookCategoryCode.Value != null ? this.LibraryBookCategoryCode.Value : null;
            dto.BookCodeSequenceNo = this.BookCodeSequenceNo;
            dto.BookCode = this.BookCode;
            dto.PlaceOfPublication = this.PlaceOfPublication;
            //dto.Subject = string.IsNullOrEmpty(this.Subjects.Key) ? (string)null : string.Format(this.Subjects.Key);
            //List<KeyValueDTO> BookCategoriesList = new List<KeyValueDTO>();

            //foreach (KeyValueViewModel vm in this.BookCategories)
            //{
            //    BookCategoriesList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
            //        );
            //}
            //dto.BookCategories = BookCategoriesList;

            //for (int i = 0; i < this.Quatity; i++)
            //{
            //    var incrtmnt = i + 1;

            //    dto.LibraryBookMaps.Add(new LibraryBookMapDTO()
            //    { 
            //        Call_No = this.Call_No,
            //        Acc_No = this.Acc_No + incrtmnt,
            //    });
            //}

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookDTO>(jsonString);
        }
    }
}

