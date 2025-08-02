using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class AssetDisposalMasterViewModel : BaseMasterViewModel
    {
        public AssetDisposalMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType= new KeyValueViewModel();
        }


        public long HeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<byte> ProcessingStatusID { get; set; }
               
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "Numeric", false)]
        [LookUp("LookUps.DocumentType")]
        [DisplayName("Document")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }
               
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String EntryDate { get; set; }
        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
     
        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

      
    }
}
