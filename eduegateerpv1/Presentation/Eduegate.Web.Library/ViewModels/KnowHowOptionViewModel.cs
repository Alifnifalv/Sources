using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class KnowHowOptionViewModel
    {
        public long KnowHowOptionIID { get; set; }
        public string KnowHowOptionText { get; set; }
        public bool IsEditable { get; set; }

        public string UserDefined { get; set; }

        public static KnowHowOptionViewModel ToViewModel(KnowHowOptionDTO dto)
        {
            return new KnowHowOptionViewModel()
            {
                IsEditable = dto.IsEditable,
                KnowHowOptionIID = dto.KnowHowOptionIID,
                KnowHowOptionText = dto.KnowHowOptionText
            };
        }

    }
}
