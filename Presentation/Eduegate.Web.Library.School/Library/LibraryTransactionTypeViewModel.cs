using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Library
{
    public class LibraryTransactionTypeViewModel : BaseMasterViewModel
    {
       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
      ///  [DisplayName("Id")]      

        public byte  LibraryTransactionTypeID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TransactionTypeName")]
        public string  TransactionTypeName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryTransactionTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryTransactionTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryTransactionTypeDTO, LibraryTransactionTypeViewModel>.CreateMap();
            var vm = Mapper<LibraryTransactionTypeDTO, LibraryTransactionTypeViewModel>.Map(dto as LibraryTransactionTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryTransactionTypeViewModel, LibraryTransactionTypeDTO>.CreateMap();
            var dto = Mapper<LibraryTransactionTypeViewModel, LibraryTransactionTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryTransactionTypeDTO>(jsonString);
        }
    }
}

