using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.UserDevice
{
    [DataContract]

    public class UserDeviceMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public UserDeviceMapDTO()
        {
        }

        [DataMember]
        public long UserDeviceMapIID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        [StringLength(200)]
        public string DeviceToken { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public long? RequestContentID { get; set; }

        [DataMember]
        public string LoginUserID { get; set; }
    }
}