using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class HomePageMenuDTO
    {
        [DataMember]
        public List<MenuDTO> HomeMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> LatestMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> AllJewelAccessoriesMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> JewelShopForMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> JewelCollectionMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> DesignersMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> StyledMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> SaleMenuList { get; set; }

        [DataMember]
        public List<MenuDTO> NewsMenuList { get; set; }
    }
}
