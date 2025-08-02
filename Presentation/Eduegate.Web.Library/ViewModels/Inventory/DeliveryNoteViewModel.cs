using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class DeliveryNoteViewModel : BaseMasterViewModel
    {
        public DeliveryNoteViewModel()
        {
            MasterViewModel = new DeliveryNoteMasterViewModel();
            DetailViewModel = new List<DeliveryNoteDetailViewModel>() { new DeliveryNoteDetailViewModel() };
        }

        public DeliveryNoteMasterViewModel MasterViewModel { get; set; }
        public List<DeliveryNoteDetailViewModel> DetailViewModel { get; set; }
    }
}