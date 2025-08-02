using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    //[ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetMasters", "CRUDModel.Model.MasterViewModel")]
    //[DisplayName("General")]
    public class AssetMasterViewModel : BaseMasterViewModel
    {
        public AssetMasterViewModel()
        {
            
        }
        public long AssetIID { get; set; }
        //public Nullable<long> AssetCategoryID { get; set; }
        //public string AssetCode { get; set; }
        //public string Description { get; set; }
        //public Nullable<long> AssetGlAccID { get; set; }
        //public Nullable<long> AccumulatedDepGLAccID { get; set; }
        //public Nullable<long> DepreciationExpGLAccId { get; set; }
        //public Nullable<int> DepreciationYears { get; set; }
       // public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

    }
}

