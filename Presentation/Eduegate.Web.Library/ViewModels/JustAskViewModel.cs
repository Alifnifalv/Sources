using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Web.Library.ViewModels
{
    public class JustAskViewModel : BaseMasterViewModel
    {
        public long JustAskIID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public string CaptchaKey { get; set; }

        public static JustAskDTO ToDTO(JustAskViewModel vm)
        {
            Mapper<JustAskViewModel, JustAskDTO>.CreateMap();
            var mapper = Mapper<JustAskViewModel, JustAskDTO>.Map(vm);

            return mapper;
        }

        public JustAskViewModel()
        {
            
            //CaptchaKey = ConfigurationManager.AppSettings["CaptchaKey"].ToString();
        }
    }
}
