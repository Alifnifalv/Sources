using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class SupplierQuickViewModel : BaseMasterViewModel
    {
        public long SupplierIID { get; set; }
        public KeyValueViewModel GLAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FirstName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MiddleName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string MiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LastName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LastName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SupplierCode")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string SupplierCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("VendorCR")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string VendorCR { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Email")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid Email Address")]

        public string SupplierEmail { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Telephone")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string TelephoneNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Address")]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string SupplierAddress { get; set; }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SupplierQuickViewModel, SupplierDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapper = Mapper<SupplierQuickViewModel, SupplierDTO>.Map(this);
            return mapper;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            return FromDTO(dto as SupplierDTO);
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SupplierDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SupplierQuickViewModel>(jsonString);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SupplierDTO>(jsonString);
        }

        public static SupplierQuickViewModel FromDTO(SupplierDTO dto)
        {
            Mapper<SupplierDTO, SupplierQuickViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<SupplierDTO, SupplierQuickViewModel>.Map(dto);
        }
    }
}
