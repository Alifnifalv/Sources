using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeAssign", "Contacts.FeeAssign")]
    [DisplayName("Phone")]
    public class PhoneViewModel : EntityPropertyMapViewModel
    {

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("PhoneType")]
        public string EntityProperty { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Telephone Code")]
        public override string Value1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Telephone Number")]
        public override string Value2 { get; set; }


        public static EntityPropertyMapDTO ToDTO(PhoneViewModel vm)
        {
            Mapper<PhoneViewModel, EntityPropertyMapDTO>.CreateMap();
            return Mapper<PhoneViewModel, EntityPropertyMapDTO>.Map(vm);
        }

        public static PhoneViewModel ToVM(EntityPropertyMapDTO dto)
        {
            Mapper<EntityPropertyMapDTO, PhoneViewModel>.CreateMap();
            return Mapper<EntityPropertyMapDTO, PhoneViewModel>.Map(dto);
        }

        public static List<PhoneViewModel> DefaultData(List<KeyValueDTO> values)
        {
            var data = new List<PhoneViewModel>();

            foreach (var value in values)
            {
                data.Add(new PhoneViewModel()
                {
                    EntityProperty = value.Value,
                    EntityPropertyID = int.Parse(value.Key)
                });
            }

            return data;
        }
    }
}
