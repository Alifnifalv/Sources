using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class UserJobApplicationViewModel : BaseMasterViewModel
    {
        public long JobApplicationIID { get; set; }
        public long JobID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Resume { get; set; }
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }

        public string CaptchaKey { get; set; }

        public static UserJobApplicationDTO ToDTO(UserJobApplicationViewModel vm)
        {
            Mapper<UserJobApplicationViewModel, UserJobApplicationDTO>.CreateMap();
            var mapper = Mapper<UserJobApplicationViewModel, UserJobApplicationDTO>.Map(vm);

            return mapper;
        }

        public UserJobApplicationViewModel()
        {
            //JobID = 0;
            //CaptchaKey = ConfigurationManager.AppSettings["CaptchaKey"].ToString();
        }
    }
}
