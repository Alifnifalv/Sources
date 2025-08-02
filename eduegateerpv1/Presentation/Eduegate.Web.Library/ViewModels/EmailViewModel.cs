using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ContactEmail", "Contacts.Emails")]
    [DisplayName("Email")]
    public class EmailViewModel : EntityPropertyMapViewModel
    {
        //public int EntityPropertyID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("EmailType")]
        public string EntityProperty { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Email")]
        public override string Value1 { get; set; }




        public static EntityPropertyMapDTO ToDTO(EmailViewModel vm)
        {
            Mapper<PhoneViewModel, EntityPropertyMapDTO>.CreateMap();
            return Mapper<EmailViewModel, EntityPropertyMapDTO>.Map(vm);
        }

        public static EmailViewModel ToVM(EntityPropertyMapDTO dto)
        {
            Mapper<EntityPropertyMapDTO, EmailViewModel>.CreateMap();
            return Mapper<EntityPropertyMapDTO, EmailViewModel>.Map(dto);
        }

        public static List<EmailViewModel> DefaultData(List<KeyValueDTO> values)
        {
            var data = new List<EmailViewModel>();

            foreach (var value in values)
            {
                data.Add(new EmailViewModel()
                {
                    EntityProperty = value.Value,
                    EntityPropertyID = int.Parse(value.Key)
                });
            }

            return data;
        }
    }
}
