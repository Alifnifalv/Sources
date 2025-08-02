using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity;
using Tabula;
using Tabula.Detectors;
using Tabula.Extractors;
using UglyToad.PdfPig;

using UglyToad.PdfPig.Content;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
namespace Eduegate.Domain.Repository
{
    public class DocumentRepository
    {
        public List<DocumentFile> GetDocuments(long? referenceID, int entityTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentFiles.Where(x => x.ReferenceID == referenceID
                && x.EntityTypeID == entityTypeID)
                    .Include(x => x.Employee)
                    .Include(x => x.DocumentFileStatus)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<DocumentFileStatus> GetDocumentFileStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentFileStatuses
                    .OrderBy(a => a.StatusName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public bool SaveDocuments(List<DocumentFile> files)
        {
            var exit = false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var file in files)
                {
                    dbContext.DocumentFiles.Add(file);
                    if (file.DocumentFileIID <= 0)
                        dbContext.Entry(file).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(file).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
                // Set exit True
                exit = true;
            }
            return exit;
        }
        public static DataSet ExtractGrid(byte[] fileBytes, string algorithemType)
        {
            using (PdfDocument document = PdfDocument.Open(fileBytes, new ParsingOptions() { ClipPaths = true }))
            {
                var dataSet = new DataSet();

                for (int pageNumber = 0; pageNumber < document.NumberOfPages; pageNumber++)
                {
                    var objectExtractor = new ObjectExtractor(document);
                    PageArea page = objectExtractor.Extract(pageNumber + 1);

                    IExtractionAlgorithm ea = null;
                    System.Collections.Generic.IReadOnlyList<TableRectangle> regions = null;

                    switch (algorithemType)
                    {
                        case "nurminen":
                            {
                                // detect canditate table zones
                                var detector = new NurminenDetectionAlgorithm();
                                regions = detector.Detect(page);
                                if (regions.Count > 0)
                                {
                                    ea = new BasicExtractionAlgorithm();
                                }
                            }
                            break;

                        case "spreadhsheet":
                            {
                                // detect canditate table zones
                                var detector = new SpreadsheetDetectionAlgorithm();
                                regions = detector.Detect(page);
                                if (regions.Count > 0)
                                {
                                    ea = new SpreadsheetExtractionAlgorithm();
                                }
                            }
                            break;
                        case "simplenurminen":
                        default:
                            {
                                // detect canditate table zones
                                var detector = new SimpleNurminenDetectionAlgorithm();
                                regions = detector.Detect(page);
                                if (regions.Count > 0)
                                {
                                    ea = new BasicExtractionAlgorithm();
                                }
                            }
                            break;
                    }

                    var tables = ea.Extract(page.GetArea(regions[0].BoundingBox)); // take first candidate area

                    if (tables != null && tables.Count > 0)
                    {
                        int tableIndex = 0;
                        foreach (var table in tables)
                        {
                            bool isFirst = true;
                            var data = new DataTable();

                            foreach (var row in table.Rows)
                            {
                                string firstCellText = row[0].GetText().Trim();

                                if (IsFooterRow(firstCellText))
                                {
                                    continue;
                                }

                                if (isFirst)
                                {
                                    // Initialize columns if it's the first row
                                    for (int i = 0; i < row.Count; i++)
                                    {
                                        var cellText = row[i].GetText().Trim().Replace(" ", "");
                                        // Add columns based on header names
                                        if (cellText.Contains("No") && cellText.Contains("PostDate"))
                                        {
                                            if (!data.Columns.Contains("No"))
                                            {
                                                data.Columns.Add("No");
                                            }
                                            if (!data.Columns.Contains("PostDate"))
                                            {
                                                data.Columns.Add("PostDate");
                                            }
                                        }
                                        else
                                        {
                                            if (!data.Columns.Contains(cellText))
                                            {
                                                data.Columns.Add(cellText);
                                            }
                                        }
                                    }
                                    isFirst = false;
                                }
                                else
                                {
                                    var dataRow = data.NewRow();
                                    int index = 0;

                                    for (int i = 0; i < row.Count; i++)
                                    {
                                        var cellText = row[i].GetText().Trim();

                                        if (i == 0 && IsMergedNoPostDate(cellText))
                                        {
                                            var splitValues = SplitNoPostDate(cellText);
                                            dataRow[index++] = splitValues[0];  // "No"
                                            dataRow[index++] = splitValues[1];  // "Post Date"
                                        }
                                        else if (i == 1) // Assuming the Description is at index 1
                                        {
                                            if (string.IsNullOrEmpty(dataRow["Description"].ToString()) && !string.IsNullOrEmpty(cellText))
                                            {
                                                dataRow["Description"] = cellText; // Assign the description correctly
                                            }
                                            else
                                            {
                                                // Merge case where description was split
                                                dataRow["Description"] = $"{dataRow["Description"]} {cellText}".Trim();
                                            }
                                        }
                                        else
                                        {
                                            // Continue assigning other columns as usual
                                            dataRow[index++] = cellText;
                                        }
                                    }

                                    while (index < data.Columns.Count)
                                    {
                                        dataRow[index++] = string.Empty;
                                    }

                                    data.Rows.Add(dataRow);
                                }
                            }

                            // Call MergeSplitRows to handle cases where descriptions are still split across rows
                            MergeSplitRows(data);
                            tableIndex += 1;
                            data.TableName = $"page_{pageNumber}_table_{tableIndex}";
                            dataSet.Tables.Add(data);
                        }
                    }
                }

                return dataSet;
            }
        }

        public static string ExtractHeaderToJson(byte[] fileBytes, string algorithemType)
        {
            using (PdfDocument document = PdfDocument.Open(fileBytes))
            {
                var dataSet = new DataSet();

                for (int pageNumber = 0; pageNumber < document.NumberOfPages; pageNumber++)
                {
                    UglyToad.PdfPig.Content.Page page = document.GetPage(pageNumber + 1);
                    string pageText = page.Text;

                    // Clean up the text by removing unwanted characters like newlines or extra spaces
                    string cleanedText = CleanUpText(pageText);

                    ExtractDynamicKeyValuePairs(cleanedText, dataSet);
                }

                return JsonConvert.SerializeObject(dataSet, Formatting.Indented);
            }
        }

        private static string CleanUpText(string text)
        {
            // Example cleanup - remove newlines and extra spaces, adjust any overlaps in the text
            return Regex.Replace(text, @"\s{2,}", " ").Trim();
        }

        private static void ExtractDynamicKeyValuePairs(string pageText, DataSet dataSet)
        {
            DataTable keyValueTable;

            if (dataSet.Tables.Contains("BankTransHeader"))
            {
                keyValueTable = dataSet.Tables["BankTransHeader"];
            }
            else
            {
                keyValueTable = new DataTable("BankTransHeader");
                dataSet.Tables.Add(keyValueTable);
                keyValueTable.Columns.Add("IBAN", typeof(string));
                keyValueTable.Columns.Add("AccountType", typeof(string));
                keyValueTable.Columns.Add("AccountName", typeof(string));
                keyValueTable.Columns.Add("CurrentBalance", typeof(string));
                keyValueTable.Columns.Add("FromDate", typeof(string));
                keyValueTable.Columns.Add("ToDate", typeof(string));
            }


            var ibanMatch = Regex.Match(pageText, @"IBAN\s*:\s*(QA\d{2}[A-Z0-9]+)(?=\s*Account Type\s*:)", RegexOptions.IgnoreCase);// TODO make it generic
            var accountTypeMatch = Regex.Match(pageText, @"Account\s*Type\s*:\s*([A-Z\s\-]+)", RegexOptions.IgnoreCase);
            var accountNameMatch = Regex.Match(pageText, @"Account\s*Name\s*:\s*([A-Z\s]+)", RegexOptions.IgnoreCase);
            var currentBalanceMatch = Regex.Match(pageText, @"Current\s*Balance\s*:\s*([\d,\.]+)", RegexOptions.IgnoreCase);
            var fromDateMatch = Regex.Match(pageText, @"From\s*Date\s*:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase);
            var toDateMatch = Regex.Match(pageText, @"To\s*Date\s*:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase);

            if (ibanMatch.Success)
            {
                DataRow row = keyValueTable.NewRow();
                row["IBAN"] = ibanMatch.Groups[1].Value.Trim();
                if (accountTypeMatch.Success) row["AccountType"] = accountTypeMatch.Groups[1].Value.Trim();
                if (accountNameMatch.Success) row["AccountName"] = accountNameMatch.Groups[1].Value.Trim();
                if (currentBalanceMatch.Success) row["CurrentBalance"] = currentBalanceMatch.Groups[1].Value.Trim();
                if (fromDateMatch.Success) row["FromDate"] = fromDateMatch.Groups[1].Value.Trim();
                if (toDateMatch.Success) row["ToDate"] = toDateMatch.Groups[1].Value.Trim();

                keyValueTable.Rows.Add(row);
            }
        }
        public static string ExtractGridToJson(byte[] fileBytes, string algorithmType)
        {
            using (PdfDocument document = PdfDocument.Open(fileBytes, new ParsingOptions() { ClipPaths = true }))
            {
                var dataSet = new DataSet();

                for (int pageNumber = 0; pageNumber < document.NumberOfPages; pageNumber++)
                {
                    var objectExtractor = new ObjectExtractor(document);
                    PageArea page = objectExtractor.Extract(pageNumber + 1);

                    IExtractionAlgorithm extractionAlgorithm = null;
                    System.Collections.Generic.IReadOnlyList<TableRectangle> regions = null;

                    switch (algorithmType.ToLower())
                    {
                        case "nurminen":
                            var nurminenDetector = new NurminenDetectionAlgorithm();
                            regions = nurminenDetector.Detect(page);
                            extractionAlgorithm = regions.Count > 0 ? new BasicExtractionAlgorithm() : null;
                            break;
                        case "spreadsheet":
                            var spreadsheetDetector = new SpreadsheetDetectionAlgorithm();
                            regions = spreadsheetDetector.Detect(page);
                            extractionAlgorithm = regions.Count > 0 ? new SpreadsheetExtractionAlgorithm() : null;
                            break;
                        case "simplenurminen":
                        default:
                            var simpleDetector = new SimpleNurminenDetectionAlgorithm();
                            regions = simpleDetector.Detect(page);
                            extractionAlgorithm = regions.Count > 0 ? new BasicExtractionAlgorithm() : null;
                            break;
                    }

                    if (extractionAlgorithm != null && regions.Count > 0)
                    {
                        var tables = extractionAlgorithm.Extract(page.GetArea(regions[0].BoundingBox));

                        if (tables != null && tables.Count > 0)
                        {
                            foreach (var table in tables)
                            {
                                var dataTable = new DataTable();
                                bool isHeaderRow = true;

                                foreach (var row in table.Rows)
                                {
                                    if (isHeaderRow)
                                    {
                                        // Set the first row as header columns
                                        for (int i = 0; i < row.Count; i++)
                                        {
                                            string columnName = row[i].GetText().Trim();
                                            if (string.IsNullOrEmpty(columnName))
                                            {
                                                columnName = $"Column_{i + 1}";
                                            }
                                            dataTable.Columns.Add(columnName);
                                        }
                                        isHeaderRow = false; // Mark header row as processed
                                    }
                                    else
                                    {
                                        var dataRow = dataTable.NewRow();
                                        for (int i = 0; i < row.Count; i++)
                                        {
                                            dataRow[i] = row[i].GetText().Trim();
                                        }
                                        dataTable.Rows.Add(dataRow);
                                    }
                                }
                               var datatablecopy= MergeTransactionDescriptions(dataTable);
                                dataSet.Tables.Add(datatablecopy);
                            }
                        }
                    }
                }
               
                return JsonConvert.SerializeObject(dataSet, Formatting.Indented);
            }
        }


        public static DataTable MergeTransactionDescriptions(DataTable dataTable)
        {
            string currentDescription = string.Empty;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];
                bool hasValueInOtherColumns = false;

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    if (!dataTable.Columns[col].ColumnName.Contains("Description") &&
                        row[col] != DBNull.Value && !string.IsNullOrWhiteSpace(row[col].ToString()))
                    {
                        hasValueInOtherColumns = true;
                        break;
                    }
                }
                if (hasValueInOtherColumns)
                {                    
                    currentDescription = GetDescriptionValue(row);
                }
                else
                {                    
                    currentDescription += " " + GetDescriptionValue(row);
                   
                    if (row.Table.Columns.Contains("Transaction Description"))
                    {
                        row["Transaction Description"] = currentDescription;
                    }
                    else if (row.Table.Columns.Contains("Description"))
                    {
                        row["Description"] = currentDescription;
                    }
                    
                    dataTable.Rows[i - 1][row.Table.Columns.Contains("Transaction Description") ? "Transaction Description" : "Description"] = currentDescription;
                    dataTable.Rows.Remove(row); 
                    i--; 
                }
            }
            return dataTable;
        }
        private static string GetDescriptionValue(DataRow row)
        {
           
            if (row.Table.Columns.Contains("Transaction Description"))
            {
                return row["Transaction Description"].ToString();
            }
            else if (row.Table.Columns.Contains("Description"))
            {
                return row["Description"].ToString();
            }
            return string.Empty; 
        }

        //public static string ExtractGridToJson(byte[] fileBytes, string algorithemType)
        //{
        //    using (PdfDocument document = PdfDocument.Open(fileBytes, new ParsingOptions() { ClipPaths = true }))
        //    {
        //        var dataSet = new DataSet();

        //        for (int pageNumber = 0; pageNumber < document.NumberOfPages; pageNumber++)
        //        {
        //            var objectExtractor = new ObjectExtractor(document);
        //            PageArea page = objectExtractor.Extract(pageNumber + 1);

        //            IExtractionAlgorithm ea = null;
        //            System.Collections.Generic.IReadOnlyList<TableRectangle> regions = null;

        //            switch (algorithemType)
        //            {
        //                case "nurminen":
        //                    {
        //                        var detector = new NurminenDetectionAlgorithm();
        //                        regions = detector.Detect(page);
        //                        if (regions.Count > 0)
        //                        {
        //                            ea = new BasicExtractionAlgorithm();
        //                        }
        //                    }
        //                    break;

        //                case "spreadhsheet":
        //                    {
        //                        var detector = new SpreadsheetDetectionAlgorithm();
        //                        regions = detector.Detect(page);
        //                        //if (regions.Count == 0) // Fallback to another algorithm if no regions are detected
        //                        //{
        //                        //    detector = new NurminenDetectionAlgorithm();
        //                        //    regions = detector.Detect(page);
        //                        //}
        //                        if (regions.Count > 0)
        //                        {
        //                            ea = new SpreadsheetExtractionAlgorithm();
        //                        }
        //                    }
        //                    break;

        //                case "simplenurminen":
        //                default:
        //                    {
        //                        var detector = new SimpleNurminenDetectionAlgorithm();
        //                        regions = detector.Detect(page);
        //                        if (regions.Count > 0)
        //                        {
        //                            ea = new BasicExtractionAlgorithm();
        //                        }
        //                    }
        //                    break;
        //            }

        //            var tables = ea.Extract(page.GetArea(regions[0].BoundingBox));

        //            if (tables != null && tables.Count > 0)
        //            {
        //                int tableIndex = 0;

        //                foreach (var table in tables)
        //                {

        //                    string targetTableName = "BankTransDetail";
        //                    DataTable data = null;

        //                    if (dataSet.Tables.Contains(targetTableName))
        //                    {
        //                        data = dataSet.Tables[targetTableName];
        //                    }
        //                    else
        //                    {
        //                        data = new DataTable(targetTableName);
        //                        dataSet.Tables.Add(data);
        //                    }
        //                    bool isFirst = true;
        //                    int columnCount = 0;
        //                    //MergeSplitRows(data);

        //                    foreach (var row in table.Rows)
        //                    {
        //                        string firstCellText = row[0].GetText().Trim();

        //                        // Skip empty rows or footer rows
        //                        if (string.IsNullOrEmpty(firstCellText) || IsFooterRow(firstCellText))
        //                        {
        //                            continue;
        //                        }

        //                        if (isFirst)
        //                        {
        //                            // Initialize columns based on the first row
        //                            for (int i = 0; i < row.Count; i++)
        //                            {
        //                                var cellText = row[i].GetText().Trim().Replace(" ", "");
        //                                if (cellText == "No.")
        //                                    cellText = "No";
        //                                if (cellText.Contains("No") && cellText.Contains("PostDate"))
        //                                {
        //                                    if (!data.Columns.Contains("No"))
        //                                    {
        //                                        data.Columns.Add("No");
        //                                    }
        //                                    if (!data.Columns.Contains("PostDate"))
        //                                    {
        //                                        data.Columns.Add("PostDate");
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (!data.Columns.Contains(cellText))
        //                                    {
        //                                        data.Columns.Add(cellText);
        //                                    }
        //                                }
        //                            }
        //                            isFirst = false;
        //                        }
        //                        else
        //                        {
        //                            var dataRow = data.NewRow();
        //                            int index = 0;

        //                            // Iterate through the cells of the row
        //                            for (int i = 0; i < row.Count; i++)
        //                            {
        //                                var cellText = row[i].GetText().Trim();

        //                                // Debug logging to track value assignments
        //                                Console.WriteLine($"Row {data.Rows.Count + 1}, Column {i}: '{cellText}'");

        //                                // Assign values based on the expected column layout
        //                                if (i == 0) // "No"
        //                                {
        //                                    if (cellText.Length >= 8 && DateTime.TryParse(cellText.Substring(cellText.Length - 8), out DateTime parsedDate))
        //                                    {
        //                                        // Split the value
        //                                        dataRow["No"] = cellText.Substring(0, cellText.Length - 8); // Extract No
        //                                        dataRow["PostDate"] = parsedDate; // Assign the parsed DateTime
        //                                    }
        //                                    else if (int.TryParse(cellText, out int noValue))
        //                                    {
        //                                        dataRow["No"] = noValue; // Assign as integer if it's valid
        //                                    }
        //                                    else
        //                                    {
        //                                        // Handle unexpected value
        //                                        dataRow["No"] = string.Empty; // Or some error handling
        //                                    }
        //                                }
        //                                else if (i == 1) // "PostDate"
        //                                {
        //                                    dataRow["PostDate"] = cellText; // Second column should be "PostDate"
        //                                }
        //                                else if (i == 2) // "ValueDate"
        //                                {
        //                                    // Expecting a combined value
        //                                    dataRow["ValueDate"] = cellText; // Assume this is ValueDate
        //                                }
        //                                else if (i == 3) // "Description"
        //                                {
        //                                    dataRow["Description"] = cellText; // Assign Description directly
        //                                }
        //                                else if (i == 4) // "DebitAmount"
        //                                {
        //                                    // Check for the case where DebitAmount is actually null or missing
        //                                    if (cellText == "-")
        //                                    {
        //                                        dataRow["DebitAmount"] = null; // Assign null for missing values
        //                                    }
        //                                    else
        //                                    {
        //                                        dataRow["DebitAmount"] = cellText; // Assign DebitAmount
        //                                    }
        //                                }
        //                                else if (i == 5) // "CreditAmount"
        //                                {
        //                                    if (cellText == "-")
        //                                    {
        //                                        dataRow["CreditAmount"] = null; // Assign null for missing values
        //                                    }
        //                                    else
        //                                    {
        //                                        dataRow["CreditAmount"] = cellText; // Assign CreditAmount
        //                                    }
        //                                }
        //                                else if (i == 6) // "Balance"
        //                                {
        //                                    dataRow["Balance"] = cellText; // Assign Balance
        //                                }
        //                            }

        //                            // Fill any remaining columns with empty values
        //                            //while (index < data.Columns.Count)
        //                            //{
        //                            //    dataRow[index++] = string.Empty;
        //                            //}

        //                            // Add the populated data row to the DataTable
        //                            data.Rows.Add(dataRow);
        //                        }
        //                    }

        //                    // Call MergeSplitRows to ensure final adjustments
        //                    MergeSplitRows(data);


        //                    tableIndex++;

        //                    //data.TableName = "BankTransDetail";
        //                    // dataSet.Tables.Add(data);
        //                }
        //            }
        //        }



        //        string jsonResult = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
        //        return jsonResult;
        //    }
        //}
        private static bool IsMergedNoPostDate(string cellText)
        {

            return System.Text.RegularExpressions.Regex.IsMatch(cellText, @"\d{2}/\d{2}/\d{2}"); // dd/mm/yy pattern
        }
        private static string[] SplitNoPostDate(string cellText)
        {

            if (cellText.Length >= 8)
            {

                int firstPartLength = cellText.Length - 8;


                string firstPart = cellText.Substring(0, firstPartLength);


                string secondPart = cellText.Substring(cellText.Length - 8);


                return new string[] { firstPart, secondPart };
            }
            else
            {
                return new string[0];
            }
        }
        //private static void MergeSplitRows(DataTable data)
        //{
        //    for (int i = 1; i < data.Rows.Count; i++)
        //    {
        //        var currentRow = data.Rows[i];
        //        var previousRow = data.Rows[i - 1];

        //        if (
        //            string.IsNullOrEmpty(currentRow["PostDate"].ToString()) &&
        //            string.IsNullOrEmpty(currentRow["ValueDate"].ToString()) &&
        //            !string.IsNullOrEmpty(currentRow["Description"].ToString()))
        //        {
        //            previousRow["Description"] = previousRow["Description"].ToString() + currentRow["Description"].ToString();

        //            data.Rows.Remove(currentRow);
        //            i--;
        //        }
        //    }
        //}

        private static void MergeSplitRows(DataTable data)
        {
            for (int i = 1; i < data.Rows.Count; i++)
            {
                var currentRow = data.Rows[i];
                var previousRow = data.Rows[i - 1];


                bool isContinuation =
                    string.IsNullOrEmpty(currentRow["PostDate"].ToString()) &&
                    string.IsNullOrEmpty(currentRow["ValueDate"].ToString()) &&
                    string.IsNullOrEmpty(currentRow["DebitAmount"].ToString()) &&
                    string.IsNullOrEmpty(currentRow["CreditAmount"].ToString()) &&
                    string.IsNullOrEmpty(currentRow["Balance"].ToString());

                if (isContinuation && !string.IsNullOrEmpty(currentRow["Description"].ToString()))
                {

                    previousRow["Description"] =
                        $"{previousRow["Description"]} {currentRow["Description"]}".Trim();


                    data.Rows.Remove(currentRow);
                    i--;
                }
            }
        }

        private static bool IsFooterRow(string cellText)
        {

            return cellText.Contains("Page") || cellText.Contains("Generated") || cellText.Contains("End of Statement");
        }


    }
}
