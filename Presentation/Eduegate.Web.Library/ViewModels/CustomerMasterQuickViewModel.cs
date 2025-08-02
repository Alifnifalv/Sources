using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    //[Bind(Exclude = "Status")]
    public class CustomerMasterQuickViewModel : BaseMasterViewModel
    {
        private static string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        public CustomerMasterQuickViewModel()
        {
        }

        public long CustomerIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Name")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string FirstName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustomerEmail { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Telephone")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string TelephoneNumber { get; set; }

        public int StatusID { get; set; }

        public bool IsPassword { get; set; }
        public string CustomerName { get; set; }
        public KeyValueViewModel GLAccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CustomerTRN")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        // set default value today date
        public string CustomerCR { get; set; }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CustomerMasterQuickViewModel, CustomerDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapper = Mapper<CustomerMasterQuickViewModel, CustomerDTO>.Map(this);
            return mapper;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            return FromDTO(dto as CustomerDTO);
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CustomerDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CustomerMasterQuickViewModel>(jsonString);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CustomerDTO>(jsonString);
        }

        public static CustomerMasterQuickViewModel FromDTO(CustomerDTO dto)
        {
            Mapper<CustomerDTO, CustomerMasterQuickViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            return Mapper<CustomerDTO, CustomerMasterQuickViewModel>.Map(dto);
        }
    }
}