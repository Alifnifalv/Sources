using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Comon
{
   public class SliderViewModel 
    {
        public string value { get; set; } = "Feb 2020";
        public SliderOptionViewModel options { get; set; }
    }
    public class SliderOptionViewModel 
    {

        public SliderOptionViewModel()
        {
            stepsArray = new List<SliderStepArrayViewModel>() { new SliderStepArrayViewModel() };
        }
        public bool showTicksValues { get; set; } = true;
       public List<SliderStepArrayViewModel> stepsArray { get; set; }
    }
    public class SliderStepArrayViewModel
    {
        public string value { get; set; } 
        public decimal legend { get; set; }

    }
}
