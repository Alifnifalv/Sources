using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

[DataContract]
public class CounselorHubMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
{
    public CounselorHubMapDTO()
    {
        CounselorHub = new KeyValueDTO();
        Class = new KeyValueDTO();
        Section = new KeyValueDTO();
        Student = new KeyValueDTO();

    }
    [DataMember]
    public long CounselorHubMapIID { get; set; }


    [DataMember]
    public long? CounselorHubID { get; set; }

    [DataMember]
    public int? ClassID { get; set; }

    [DataMember]
    public int? SectionID { get; set; }

    [DataMember]
    public long? StudentID { get; set; }

    [DataMember]

    public bool? AllClass { get; set; }

    [DataMember]
    public bool? AllSection { get; set; }

    [DataMember]
    public bool? AllStudent { get; set; }
    [DataMember]

    public KeyValueDTO Class { get; set; }

    [DataMember]

    public KeyValueDTO CounselorHub { get; set; }

    [DataMember]

    public KeyValueDTO Section { get; set; }

    [DataMember]

    public KeyValueDTO Student { get; set; }

    [DataMember]
    public string StudentName { get; set; }

    [DataMember]
    public long SerialNo { get; set; }

}

