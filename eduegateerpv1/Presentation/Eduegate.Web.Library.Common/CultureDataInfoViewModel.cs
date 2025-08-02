using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class CultureDataInfoViewModel
    {
        public byte CultureID { get; set; }
        public string CultureCode { get; set; }
        public string CultureName { get; set; }

        //TODO: Add AllowHtml attribute
        //[AllowHtml] 
        public string CultureValue { get; set; }
        public string TimeStamps { get; set; }

        public static List<CultureDataInfoViewModel> FromDTO(List<CultureDataInfoDTO> dtos)
        {
            var vms = new List<CultureDataInfoViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static CultureDataInfoViewModel FromDTO(CultureDataInfoDTO dto)
        {
            Mapper<CultureDataInfoDTO, CultureDataInfoViewModel>.CreateMap();
            return Mapper<CultureDataInfoDTO, CultureDataInfoViewModel>.Map(dto);
        }

        public static CultureDataInfoDTO FromVM(CultureDataInfoViewModel vm)
        {
            Mapper<CultureDataInfoViewModel, CultureDataInfoDTO>.CreateMap();
            return Mapper<CultureDataInfoViewModel, CultureDataInfoDTO>.Map(vm);
        }
    }
}