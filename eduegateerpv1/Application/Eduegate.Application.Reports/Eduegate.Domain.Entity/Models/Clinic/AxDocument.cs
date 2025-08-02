using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    public partial class AxDocument
    {
        public AxDocument()
        {
            this.AxDocument1 = new List<AxDocument>();
            //this.AxFlashControls = new List<AxFlashControl>();
            this.AxImages = new List<AxImage>();
            //this.AxLinks = new List<AxLink>();
        }

        public int AxID { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public Nullable<int> Author { get; set; }
        public Nullable<int> FormatID { get; set; }
        public Nullable<int> SourceID { get; set; }
        public System.DateTime Created { get; set; }
        public byte PublicationState { get; set; }
        public string Doc_Caption { get; set; }
        public string Doc_AdditionalString { get; set; }
        public string Doc_Description { get; set; }
        public Nullable<System.DateTime> Doc_AdditionalDate { get; set; }
        public Nullable<System.DateTime> Doc_ValidFrom { get; set; }
        public Nullable<System.DateTime> Doc_ValidTill { get; set; }
        public string Doc_Keywords { get; set; }
        public byte SourceType { get; set; }
        public Nullable<byte> SourceHandle { get; set; }
        public string SourceURL { get; set; }
        public Nullable<System.DateTime> Published { get; set; }
        public bool Publishable { get; set; }
        public int CurrentVersion { get; set; }
        public int CurrentSubVersion { get; set; }
        public Nullable<long> FileSize { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public string SourceUsername { get; set; }
        public string SourcePassword { get; set; }
        public Nullable<int> CheckedOutByUser { get; set; }
        public string KeyHeadline { get; set; }
        public string KeyDescription { get; set; }
        public string City { get; set; }
        public virtual ICollection<AxDocument> AxDocument1 { get; set; }
        public virtual AxDocument AxDocument2 { get; set; }
        //public virtual AxImageFormat AxImageFormat { get; set; }
        //public virtual AxUser AxUser { get; set; }
        //public virtual ICollection<AxFlashControl> AxFlashControls { get; set; }
        public virtual ICollection<AxImage> AxImages { get; set; }
        //public virtual ICollection<AxLink> AxLinks { get; set; }
    }
}
