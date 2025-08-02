using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class KeyValueViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static List<KeyValueViewModel> FromDTO(List<KeyValueDTO> dtos){
            var vM = new List<KeyValueViewModel>();

            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    if (dto.Key != null)
                    {
                        vM.Add(new KeyValueViewModel() { Key = dto.Key, Value = dto.Value });
                    }
                }
            }

            return vM;
        }

        public static List<KeyValueDTO> ToDTO(List<KeyValueViewModel> vms)
        {
            var vM = new List<KeyValueDTO>();

            if (vms != null)
            {
                foreach (var dto in vms)
                {
                    vM.Add(new KeyValueDTO() { Key = dto.Key, Value = dto.Value });
                }
            }

            return vM;
        }

        public static KeyValueDTO ToDTO(KeyValueViewModel viewmodel)
        {
            if (viewmodel != null)
            {
                return new KeyValueDTO()
                {
                    Key = viewmodel.Key,
                    Value = viewmodel.Value
                };
            }
            else
                return new KeyValueDTO();
        }

        public static KeyValueViewModel ToViewModel(KeyValueDTO dto)
        {
            if (dto != null)
            {
                return new KeyValueViewModel()
                {
                    Key = dto.Key,
                    Value = dto.Value
                };
            }
            else
                return new KeyValueViewModel();
        }

        public static void AddOrUpdate(List<KeyValueViewModel> parameter, string key, string value)
        {
            var exists = parameter.FirstOrDefault(a => a.Key.ToUpper() == key.ToUpper());

            if (exists != null)
                exists.Value = value;
            else
                parameter.Add(new KeyValueViewModel() { Key = key.ToUpper(), Value = value });
        }
    }
}