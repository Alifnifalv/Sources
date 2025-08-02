using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;

namespace Eduegate.Utilities.DataExportImport
{
    public static class Upload
    {
        private static string supportedFileExtensions = ".xlsx";

        public static List<string> UploadToServer(HttpRequestBase request, string uploadLocation)
        {
            List<string> uploadedFileCollection = new List<string>();
            if (ValidateUpload(request, uploadLocation))
            {
                if (!Directory.Exists(uploadLocation))
                    Directory.CreateDirectory(uploadLocation);

                foreach (string fileName in request.Files)
                {
                    HttpPostedFileBase file = request.Files[fileName];

                    if (file != null && file.ContentLength > 0)
                    {
                        string fileNameWithPath = Path.Combine(uploadLocation, file.FileName);
                        using (var fileStream = System.IO.File.Create(fileNameWithPath))
                        {
                            file.InputStream.Seek(0, SeekOrigin.Begin);
                            file.InputStream.CopyTo(fileStream);
                            file.InputStream.Close();
                            file.InputStream.Dispose();
                        }
                        uploadedFileCollection.Add(fileNameWithPath);
                    }
                }
            }

            return uploadedFileCollection;
        }

        public static List<ProductInventoryDTO> ExtractInventoryDetails(string fileToProcess)
        {
            List<ProductInventoryDTO> productInventoryCollection = new List<ProductInventoryDTO>();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(fileToProcess))
                {
                    package.Load(stream);
                }
                var ws = package.Workbook.Worksheets.First();


                var startRow = 2;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {

                    if (ws.Cells[rowNum, 1].Text.Length > 0 && ws.Cells[rowNum, 3].Text.Length > 0)
                    {
                        var skuMapIID = Convert.ToInt64(ws.Cells[rowNum, 1].Text);
                        var quantity = Convert.ToInt64(ws.Cells[rowNum, 3].Text);
                        var batch = Convert.ToInt64(ws.Cells[rowNum, 4].Text);
                        var branchID = Convert.ToInt64(ws.Cells[rowNum, 5].Text);
                        productInventoryCollection.Add(new ProductInventoryDTO()
                        { ProductSKUMapID = skuMapIID, Quantity = quantity, Batch = batch, BranchID = branchID });
                    }
                }
            }
            return productInventoryCollection;
        }

        private static bool ValidateUpload(HttpRequestBase request, string uploadLocation)
        {

            if (request.Files.IsNull() || request.Files.Count < 1)
                throw new Exception("No file(s) selected to upload!");

            if (uploadLocation.IsNullOrEmpty())
                throw new Exception("Upload location has to be supplied!");

            foreach (string fileName in request.Files)
            {
                HttpPostedFileBase file = request.Files[fileName];
                if (System.IO.Path.GetExtension(file.FileName) != supportedFileExtensions)
                    throw new Exception(string.Format("Only {0} are supported!", supportedFileExtensions));
            }

            return true;
        }
    }
}
