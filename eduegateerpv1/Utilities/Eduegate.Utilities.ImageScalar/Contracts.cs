using Eduegate.Framework.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Utilities.ImageScalar
{
    public class Contracts
    {
        public sealed class ImageAttributes
        {
            public string SourceImageName { get; set; }
            public string SourcePath { get; set; }
            public string DestinationImageName { get; set; }
            public string DestinationImagePath { get; set; }
            public ImageType DestinationImageType { get; set; }
            public int DestinationImageWidth { get; set; }
            public int DestinationImageHeight{ get; set; }
            public bool IsResized { get { return IsSuccess; } }
            internal bool IsSuccess { get; set; }
        }
    }
}
