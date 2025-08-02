using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using Eduegate.Framework.Extensions;
using System.Data;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Utilities.DataExportImport
{
    public static class Export
    {
        public static string ToExcel(Eduegate.Services.Contracts.Search.SearchResultDTO searchResult, string exportPath, string exportFileName)
        {
            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            FileInfo newFile = new FileInfo(Path.Combine(exportPath, exportFileName));

            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(Path.Combine(exportPath, Path.GetFileNameWithoutExtension(exportFileName) + ".xlsx"));
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                int columnIndex = 1;
                // Add a worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(exportFileName);
                foreach (Eduegate.Services.Contracts.Search.ColumnDTO column in searchResult.Columns)
                {

                    worksheet.Cells[1, columnIndex].Value = column.IsNull() ? string.Empty : column.ColumnName;
                    columnIndex++;
                }

                columnIndex = 1;
                int rowIndex = 2;
                foreach (DataRowDTO row in searchResult.Rows)
                {
                    columnIndex = 1;
                    foreach (object column in row.DataCells)
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = column.IsNull() ? string.Empty : column.ToString();
                        columnIndex++;
                    }
                    rowIndex++;
                }
                package.Save();
            }
            return newFile.Name;

        }

        public static string ToExcel(DataSet dataSet, string exportPath, string exportFileName)
        {
            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            FileInfo newFile = new FileInfo(Path.Combine(exportPath, exportFileName));

            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(Path.Combine(exportPath, Path.GetFileNameWithoutExtension(exportFileName) + ".xlsx"));
            }

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                int columnIndex = 1;
                // Add a worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(exportFileName);
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {

                        worksheet.Cells[1, columnIndex].Value = column.Caption;
                        columnIndex++;
                    }
                }

                columnIndex = 1;
                int rowIndex = 2;

                foreach (DataTable dataTable in dataSet.Tables)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        columnIndex = 1;
                        for (int columnCounter = 0; columnCounter <= dataTable.Columns.Count - 1; columnCounter++)
                        {
                            worksheet.Cells[rowIndex, columnIndex].Value = row[columnCounter].ToString();
                            columnIndex++;
                        }
                        rowIndex++;
                    }
                }

                package.Save();
            }

            return newFile.Name;
        }

        public static string ToExcelTemplate(List<string> columnNames, string exportPath, string exportFileName, string pageName = null)
        {
            pageName = pageName.IsNullOrEmpty() ? exportFileName : pageName;
            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            if (Path.GetExtension(exportFileName).IsNullOrEmpty())
                exportFileName = string.Concat(Path.GetFileNameWithoutExtension(exportFileName), "_", Guid.NewGuid(), ".xlsx");

            FileInfo newFile = new FileInfo(Path.Combine(exportPath, exportFileName));
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(Path.Combine(exportPath, exportFileName));
            }

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                int columnNumber = 1;
                // Add a worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(pageName);
                foreach (string columnName in columnNames)
                {

                    worksheet.Cells[1, columnNumber].Value = columnName;
                    columnNumber++;
                }

                package.Save();
            }

            return newFile.Name;
        }

        public static DataSet ToDataSet(List<string> columns, string fileToRead, int cellCount)
        {
            var dataSet = new DataSet("DataFeed");

            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(fileToRead))
                {
                    package.Load(stream);
                }

                foreach (var ws in package.Workbook.Worksheets)
                {
                    var dataTable = new DataTable("FeedTable" + ws.Index);
                    var rowCounter = 1;

                    for (int rowNum = rowCounter; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        if (rowCounter == 1)
                        {
                            foreach (var column in columns)
                            {
                                dataTable.Columns.Add(new DataColumn() { ColumnName = column });
                            }
                        }
                        else
                        {
                            int columnCounter = 0;
                            DataRow newRow = dataTable.NewRow();
                            for (int cellNum = 1; cellNum <= cellCount; cellNum++)
                            {
                                newRow[columnCounter] = ws.Cells[rowNum, cellNum].Text;
                                columnCounter++;
                            }

                            dataTable.Rows.Add(newRow);
                        }

                        rowCounter++;
                    }

                    dataSet.Tables.Add(dataTable);
                }
            }

            return dataSet;
        }

        public static string ToPDF(Eduegate.Services.Contracts.Search.SearchResultDTO searchResult, string exportPath, string exportFileName)
        {
            throw new NotImplementedException();
        }
    }
}
