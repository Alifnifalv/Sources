using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductGallery
    {
        public int ProductGalleryID { get; set; }
        public Nullable<int> RefProductGalleryID { get; set; }
        public string ProductActualImage { get; set; }
        public string ProductListingImage { get; set; }
        public string ProductThumbnail { get; set; }
        public string ProductGalleryImage { get; set; }
        public bool ProductGalleryZoom { get; set; }
        public byte ProductGalleryPosition { get; set; }
    }
}
