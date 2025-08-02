using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class KeyValueViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }


        public static List<KeyValueViewModel> FromDTO(List<KeyValueDTO> dtoList)
        {
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();           
            return Mapper< List<KeyValueDTO>, List<KeyValueViewModel>>.Map(dtoList);
        }
       
    }
}
