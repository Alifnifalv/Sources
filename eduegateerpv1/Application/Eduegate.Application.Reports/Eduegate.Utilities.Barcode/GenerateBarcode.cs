using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Utilities.Barcode
{
    public class GenerateBarcode
    {
        public void GetBacode(string _data, string _filename)
        {
            Linear barcode = new Linear();
            barcode.Type = BarcodeType.CODE11;
            barcode.Data = _data;
            barcode.drawBarcode(_filename);
        }

        public byte[] GetBarCodeImage(string Barcode, BarcodeType BarcodeType)
        {
            Linear barcode = new Linear(); // Create linear barcode object  
            barcode.Type = BarcodeType;	 // Set barcode symbology type to Code-39
            barcode.Data = Barcode; // Set barcode data to encode
            barcode.Format = ImageFormat.Jpeg;
            return barcode.drawBarcodeAsBytes(); // Draw & print generated barcode to png image file
        }

        public void GetQrcode(string _data, string _filename)
        {
            QRCode qrcode = new QRCode();
            qrcode.Data = _data;
            qrcode.DataMode = QRCodeDataMode.Byte;
            qrcode.UOM = UnitOfMeasure.PIXEL;
            qrcode.X = 3;
            qrcode.LeftMargin = 0;
            qrcode.RightMargin = 0;
            qrcode.TopMargin = 0;
            qrcode.BottomMargin = 0;
            qrcode.Resolution = 72;
            qrcode.Rotate = Rotate.Rotate0;
            qrcode.ImageFormat = ImageFormat.Gif;
            qrcode.drawBarcode(_filename);
        }

        public byte[] Get128BacCodeImage(string Barcode)
        {
            Linear barcode = new Linear(); // Create linear barcode object  
            barcode.Type = BarcodeType.CODE128;	 // Set barcode symbology type to Code-39
            barcode.Data = Barcode; // Set barcode data to encode
            barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;
            return barcode.drawBarcodeAsBytes(); // Draw & print generated barcode to png image file
        }
    }
}
