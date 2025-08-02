using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class MissionProcessingViewModel : BaseMasterViewModel
    {
        public MissionProcessingViewModel()
        {
            MasterViewModel = new MissionProcessingHeadViewModel();
            DetailViewModel = new List<MissionProcessingDetailViewModel>();
        }

        public List<UrlViewModel> Urls { get; set; }
        public MissionProcessingHeadViewModel MasterViewModel { get; set; }
        public List<MissionProcessingDetailViewModel> DetailViewModel { get; set; }

        public string SaveURL { get; set; }
    }
}
