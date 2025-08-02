using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Data;
using System.ServiceModel.Channels;

namespace Eduegate.Domain.Mappers.School.Library
{
    public class LibraryTransactionMapper : DTOEntityDynamicMapper
    {
        public static LibraryTransactionMapper Mapper(CallContext context)
        {
            var mapper = new LibraryTransactionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryTransactionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LibraryTransactionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryTransactions.Where(x => x.LibraryTransactionIID == IID)
                    .Include(i => i.Student)
                    .Include(i => i.Employee)
                    .Include(i => i.LibraryBookMap).ThenInclude(i => i.LibraryBook)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new LibraryTransactionDTO()
                {
                    LibraryTransactionIID = entity.LibraryTransactionIID,
                    LibraryTransactionTypeID = entity.LibraryTransactionTypeID,
                    Notes = entity.Notes,
                    BookID = entity.BookID,
                    LibraryBookMapID = entity.LibraryBookMapID,
                    Book = new KeyValueDTO()
                    {
                        Key = entity.LibraryBookMapID.ToString(),
                        Value = entity.LibraryBookMap?.Acc_No + " - " + entity.LibraryBookMap.LibraryBook?.BookTitle
                    },
                    EmployeeID = entity.EmployeeID,
                    StaffName = entity.Employee != null ? entity.Employee?.EmployeeCode + " - " + entity.Employee?.FirstName + " " + entity.Employee?.MiddleName + " " + entity.Employee?.LastName : null,
                    StudentID = entity.StudentID,
                    Student = new KeyValueDTO()
                    {
                        Key = entity.StudentID.ToString(),
                        Value = (entity.Student == null ? null : entity.Student?.AdmissionNumber + " - " + entity.Student?.FirstName + ' ' + entity.Student?.MiddleName + ' ' + entity.Student?.LastName)
                    },
                    TransactionDate = entity.TransactionDate,
                    ReturnDueDate = entity.ReturnDueDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    BookConditionID = entity.BookCondionID,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    //Acc_No = entity.LibraryBook.Acc_No,
                    Call_No = entity.Call_No,

                    ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    //IsApplyLateFee = entity.IsApplyLateFee.HasValue ? entity.IsApplyLateFee.Value : (bool?)null,
                    //LateFeeAmount = entity.LateFeeAmount.HasValue ? entity.LateFeeAmount.Value : (decimal?)null,
                    //IsApplyDamageCost = entity.IsApplyDamageCost.HasValue ? entity.IsApplyDamageCost.Value : (bool?)null,
                    //PercentageDamageCost = entity.PercentageDamageCost.HasValue ? entity.PercentageDamageCost.Value : (decimal?)null,
                };
                dto.BookIssueDetails = GetIssuedLibraryBookDetails(dto.Call_No, dto.LibraryBookMapID).BookIssueDetails;
                dto.AvailableBookQty = GetAvailableBookDetails(dto.Call_No, dto.LibraryBookMapID).AvailableBookQty;

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var toDto = dto as LibraryTransactionDTO;

            if (toDto.LibraryBookMapID == null)
            {
                throw new Exception("Please Select Any Book");
            }

            if (toDto.StudentID == null && toDto.EmployeeID == null)
            {
                throw new Exception("Please Select Any Student or Employee !");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new LibraryTransaction()
            {
                //Acc_No = toDto.Acc_No,
                Call_No = toDto.Call_No,
                Notes = toDto.Notes,
                //BookID = toDto.BookID,
                LibraryBookMapID = toDto.LibraryBookMapID,
                StudentID = toDto.StudentID,
                EmployeeID = toDto.EmployeeID,
                ReturnDueDate = toDto.ReturnDueDate,
                TransactionDate = toDto.TransactionDate,
                BookCondionID = toDto.BookConditionID,
                LibraryTransactionIID = toDto.LibraryTransactionIID,
                LibraryTransactionTypeID = toDto.LibraryTransactionTypeID,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                //IsApplyLateFee = toDto.IsApplyLateFee.HasValue ? toDto.IsApplyLateFee.Value : (bool?)null,
                //LateFeeAmount = toDto.LateFeeAmount.HasValue ? toDto.LateFeeAmount.Value : (decimal?)null,
                CreatedBy = toDto.LibraryTransactionIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LibraryTransactionIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LibraryTransactionIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LibraryTransactionIID > 0 ? DateTime.Now : dto.UpdatedDate,
                ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),                   
                //IsApplyDamageCost = toDto.IsApplyDamageCost.HasValue ? toDto.IsApplyDamageCost.Value : (bool?)null,
                //PercentageDamageCost = toDto.PercentageDamageCost.HasValue ? toDto.PercentageDamageCost.Value : (decimal?)null,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //edit save block-- only create is enable
                if (toDto.LibraryTransactionIID != 0)
                {
                    throw new Exception("Edit option is not available for library transaction");
                }

                //Block duplicate book entry issue
                var issueID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("LIBRARY_TRANSACTION_TYP_ISSUE");
                var returnID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("LIBRARY_TRANSACTION_TYP_RETURN");

                var isBookAvailable = dbContext.LibraryBookMaps.Where(x => x.LibraryBookMapIID == toDto.LibraryBookMapID).AsNoTracking().FirstOrDefault();
                var transactions = dbContext.LibraryTransactions.Where(s => s.SchoolID == _context.SchoolID).Include(i => i.Student).AsNoTracking().ToList();
                var transactionDetail = transactions.Where(d => d.LibraryBookMapID == toDto.LibraryBookMapID).OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();

                entity.Call_No = isBookAvailable.Call_No;
                entity.Acc_No = isBookAvailable.Acc_No;

                //issue
                if (toDto.LibraryTransactionTypeID == issueID)
                {
                    if (isBookAvailable == null)
                    {
                        throw new Exception("This book is not available for Issue. please check with Library book screen");
                    }

                    if (transactionDetail != null && transactionDetail.LibraryTransactionTypeID != returnID)
                    {
                        var alredayIssuedTo = transactionDetail.StudentID.HasValue ?
                            transactionDetail.Student.AdmissionNumber + " - " + transactionDetail.Student.FirstName + " " + transactionDetail.Student.MiddleName + " " + transactionDetail.Student.LastName :
                            transactionDetail.Employee.EmployeeCode + " - " + transactionDetail.Employee.FirstName + " " + transactionDetail.Employee.MiddleName + " " + transactionDetail.Employee.LastName;

                        var retuenDueDate = transactionDetail.ReturnDueDate.HasValue ? transactionDetail.ReturnDueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                        throw new Exception("This book is not available, already issued to " + "<b>" + alredayIssuedTo + "</b>" + " and return due date is : " + "<b>" + retuenDueDate + "</b>");
                    }
                }

                if (toDto.LibraryTransactionTypeID == issueID)
                {
                    //check the student/staff is already had a book and it's not returned
                    var stud = toDto.StudentID.HasValue ?
                        transactions.Where(x => x.StudentID == toDto.StudentID)
                        .OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault() : null;

                    if (stud != null && stud.LibraryTransactionTypeID != returnID)
                    {
                        throw new Exception("Student already issued a book and it's not returned yet !");
                    }

                    if (toDto.EmployeeID != null)
                    {
                        List<LibraryTransactionDTO> data = new List<LibraryTransactionDTO>();

                        using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
                        {
                            conn.Open();

                            string countQuery = $"SELECT COUNT(*) FROM [schools].[LibraryTransactionSearch] WHERE ISNULL(IsStudent, 0) = 0 " +
                                $"AND LibraryTransactionTypeID != (SELECT CAST(SettingValue AS INT) FROM setting.Settings WHERE SettingCode = 'LIBRARY_TRANSACTION_TYP_RETURN')" + $"AND Stud_Emp_ID = " + toDto.EmployeeID;

                            SqlCommand countCommand = new SqlCommand(countQuery, conn);
                            int count = (int)countCommand.ExecuteScalar();

                            if (count >= 2)
                            {
                                throw new Exception("Staff already issued a book and it's not returned yet !");
                            }
                        }
                    }
                }

                //return
                if (toDto.LibraryTransactionTypeID == returnID)
                {
                    var checkTransaction = toDto.StudentID.HasValue ? 
                        dbContext.LibraryTransactions.Where(d => d.LibraryBookMapID == toDto.LibraryBookMapID && d.StudentID == toDto.StudentID)
                        .AsNoTracking().OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault()
                    : dbContext.LibraryTransactions.Where(x => x.LibraryBookMapID == toDto.LibraryBookMapID && x.EmployeeID == toDto.EmployeeID)
                    .AsNoTracking().OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();

                    var studStafName = toDto.StudentID.HasValue ? toDto.StudentName : toDto.EmployeeName;

                    if (checkTransaction == null || checkTransaction.IsReturned == true)
                    {
                        throw new Exception("This book was not issued to " + "<b>" + studStafName + "</b>" + " please cross check !");
                    }

                    entity.IsReturned = true;
                }

                //reniew
                if (toDto.LibraryTransactionTypeID != issueID && toDto.LibraryTransactionTypeID != returnID)
                {
                    var transactionType = dbContext.LibraryTransactionTypes.Where(y => y.LibraryTransactionTypeID == toDto.LibraryTransactionTypeID).AsNoTracking().FirstOrDefault();
                    if (transactionDetail != null && transactionDetail.LibraryTransactionTypeID == returnID)
                    {
                        throw new Exception("This book is already returned, can't " + transactionType.TransactionTypeName);
                    }
                    if (transactionDetail == null)
                    {
                        throw new Exception("There is no transactions against this book. please issue the book first !");
                    }

                    var checkTransaction = toDto.StudentID.HasValue ?
                        dbContext.LibraryTransactions.Where(d => d.LibraryBookMapID == toDto.LibraryBookMapID && d.StudentID == toDto.StudentID)
                        .AsNoTracking().OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault()
                    : dbContext.LibraryTransactions.Where(x => x.LibraryBookMapID == toDto.LibraryBookMapID && x.EmployeeID == toDto.EmployeeID)
                    .AsNoTracking().OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();

                    var studStafName = toDto.StudentID.HasValue ? toDto.StudentName : toDto.EmployeeName;

                    if (checkTransaction == null || checkTransaction.IsReturned == true)
                    {
                        throw new Exception("This book was not issued to " + "<b>" + studStafName + "</b>" + " please cross check !");
                    }
                }

                if (entity.LibraryTransactionIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    //Generate Workflow for Transaction status change to Return
                    if (entity.LibraryTransactionTypeID == issueID)
                    {
                        var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LIBRARY_BOOK_RETURN_WORKFLOW_ID");
                        var workflowID = long.Parse(settingValue);

                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(workflowID, entity.LibraryTransactionIID);
                    }
                }
                else
                {
                    throw new Exception("Edit option is not available for library transaction");
                }

            }
            return ToDTOString(ToDTO(entity.LibraryTransactionIID));
        }

        public LibraryTransactionDTO GetLibraryStudentFromStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new LibraryTransactionDTO();

                //Get Student details
                var stud = dbContext.Students.Where(s => s.StudentIID == studentID && s.IsActive == true).AsNoTracking().FirstOrDefault();

                if (stud != null)
                {
                    var libraryDTO = new LibraryTransactionDTO()
                    {
                        StudentID = stud.StudentIID,
                        StudentName = stud.AdmissionNumber + " - " + stud.FirstName + ' ' + stud.MiddleName + ' ' + stud.LastName,
                    };

                    return libraryDTO;
                }
                else
                {
                    return null;
                }
            }
        }

        public LibraryTransactionDTO GetLibraryStaffFromEmployeeID(long employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new LibraryTransactionDTO();

                //Get Employee details
                var employee = dbContext.Employees.Where(e => e.EmployeeIID == employeeID && e.IsActive == true).AsNoTracking().FirstOrDefault();

                if (employee != null)
                {
                    var librarysDTO = new LibraryTransactionDTO()
                    {
                        EmployeeID = employee.EmployeeIID,
                        StaffName = employee.EmployeeCode + " - " + employee.FirstName + ' ' + employee.MiddleName + ' ' + employee.LastName,
                    };

                    return librarysDTO;
                }
                else
                {
                    return null;
                }
            }
        }

        // Get Available Book Details By Call_No :
        public LibraryTransactionDTO GetAvailableBookDetails(string CallAccNo, int? bookMapID)
        {
            var dtos = new LibraryTransactionDTO();
            var listmapBooks = new List<LibraryBookMapDTO>();
            string bookMapedDetails = null;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var returnID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("LIBRARY_TRANSACTION_TYP_RETURN");

                var bookList = CallAccNo != "null" ? dbContext.LibraryBookMaps.Where(a => a.Call_No == CallAccNo || a.Acc_No == CallAccNo).AsNoTracking().ToList()
                    : dbContext.LibraryBookMaps.Where(a => a.LibraryBookMapIID == bookMapID).AsNoTracking().ToList();

                if (bookList.Count > 0)
                {
                    int S_No = 1;
                    foreach (var trans in bookList)
                    {
                        var issuedBooks = dbContext.LibraryTransactions.Where(x => x.LibraryBookMapID == trans.LibraryBookMapIID)
                            .AsNoTracking().OrderByDescending(o => o.LibraryTransactionIID).FirstOrDefault();

                        var mapedBooks = new List<LibraryBookMapDTO>();
                        if (issuedBooks == null || issuedBooks.LibraryTransactionTypeID == returnID)
                        {
                            var bk = new LibraryBookMapDTO()
                            {
                                Acc_No = trans.Acc_No,
                                S_No = S_No++,
                            };

                            listmapBooks.Add(bk);
                            bookMapedDetails = string.Concat(bookMapedDetails, bk.S_No + ") " + " Acc No : " + bk.Acc_No, "</br>");
                        }
                    }
                }
                else
                {
                    bookMapedDetails = "This Book is not Available!";
                }
                dtos.AvailableBookQty = bookMapedDetails;
            }
            return dtos;
        }


        // Get Issued Book Details By Call_No :
        public LibraryTransactionDTO GetIssuedLibraryBookDetails(string CallAccNo, int? bookMapID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var dtoss = new LibraryTransactionDTO();
            var mapBooks = new List<LibraryBookMapDTO>();
            string issuedBookDetails = null;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var returnID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("LIBRARY_TRANSACTION_TYP_RETURN");

                var bookList = !string.IsNullOrEmpty(CallAccNo) /*&& CallAccNo != "null"*/ ?
                    dbContext.LibraryBookMaps.Where(a => a.Call_No == CallAccNo || a.Acc_No == CallAccNo).AsNoTracking().ToList()
                    : dbContext.LibraryBookMaps.Where(a => a.LibraryBookMapIID == bookMapID).AsNoTracking().ToList();

                if (bookList.Count > 0)
                {
                    foreach (var trans in bookList)
                    {
                        var issuedBooks = dbContext.LibraryTransactions.Where(x => x.LibraryBookMapID == trans.LibraryBookMapIID)
                            .Include(i => i.Student)
                            .Include(i => i.Employee)
                            .OrderByDescending(o => o.LibraryTransactionIID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        var mapedBooks = new List<LibraryBookMapDTO>();
                        if (issuedBooks != null && issuedBooks.LibraryTransactionTypeID != returnID)
                        {
                            var bk = new LibraryBookMapDTO()
                            {
                                Acc_No = trans.Acc_No,
                            };

                            mapBooks.Add(bk);

                            var issuedToName = issuedBooks.StudentID.HasValue ? issuedBooks.Student.AdmissionNumber + " - " + issuedBooks.Student.FirstName + " " + issuedBooks.Student.MiddleName + " " + issuedBooks.Student.LastName : issuedBooks.Employee.EmployeeCode + " - " + issuedBooks.Employee.FirstName + " " + issuedBooks.Employee.MiddleName + " " + issuedBooks.Employee.LastName;
                            var TransactionDateString = issuedBooks.TransactionDate.HasValue ? issuedBooks.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                            issuedBookDetails = string.Concat(issuedBookDetails, "Acc No : " + "<b>" + bk.Acc_No + "</b>" + " | " + " Issued on : " + "<b>" + TransactionDateString + "</b>" + "<br>" + "Issued to : " + "<b>" + issuedToName + "</b>", "</br>");
                        }
                    }
                }

                if (issuedBookDetails == null || issuedBookDetails == "")
                {
                    issuedBookDetails = "Selected Book is not issued for anyone";
                }

                dtoss.BookIssueDetails = issuedBookDetails;
            }
            return dtoss;
        }

        //function calling for lookup filling by Call_No
        public List<KeyValueDTO> GetBookDetailsByCallNo(string CallAccNo)
        {
            var dtos = new LibraryTransactionDTO();
            var bookList = new List<KeyValueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entities = dbContext.LibraryBookMaps.Where(b => b.Call_No == CallAccNo || b.Acc_No == CallAccNo)
                    .Include(i => i.LibraryBook)
                    .AsNoTracking().ToList();

                foreach (var book in entities)
                {
                    bookList.Add(new KeyValueDTO
                    {
                        Key = book.LibraryBookMapIID.ToString(),
                        Value = book.Acc_No + " - " + book.LibraryBook.BookTitle
                    });
                }
            }
            return bookList;
        }

        //public LibraryBookDTO GetBookDetailsChange(long BookID)
        //{
        //    var book = new LibraryBookDTO();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var bookentity = dbContext.LibraryBooks.Where(b => b.LibraryBookIID == BookID).AsNoTracking().FirstOrDefault();
        //        book.Call_No = bookentity.Call_No;
        //        book.Acc_No = bookentity.Acc_No;
        //    }
        //    return book;
        //}
        public List<LibraryTransactionDTO> GetLibraryTransactionDetails(long parentID , string filter)
        {
            List<LibraryTransactionDTO> libraryTransactionDTOList = new List<LibraryTransactionDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Get all active students associated with the parent
                var studentIDs = dbContext.Students
                    .Where(s => s.ParentID == parentID && s.IsActive == true)
                    .Select(s => s.StudentIID)
                    .ToList();

                if (studentIDs.Any())
                {
                    SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                    _sBuilder.ConnectTimeout = 30; // Set timeout

                    using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        // Convert the list of student IDs to a comma-separated string
                        string studentIDList = string.Join(",", studentIDs);

                        // Query to fetch library transaction details from the view
                        string query = @$"
                    SELECT StudName, Book, TransactionDate, ReturnDueDate, CurrentStatus
                    FROM [schools].[StudentLibraryRecordView]
                    WHERE StudentID IN ({studentIDList})
                    AND CurrentStatus = '{filter}'";

                        using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                        {
                            sqlCommand.CommandType = CommandType.Text;

                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var libraryTransactionDTO = new LibraryTransactionDTO
                                    {
                                        StudentName = Convert.ToString(reader["StudName"]),
                                        // Adjusted to get the correct book title
                                        BookTitle = reader["Book"].ToString(),  // Assuming "Book" is the correct column for book title

                                        // Adjusted for correct date mapping
                                        TransactionDate = reader["TransactionDate"] != DBNull.Value
                                            ? DateTime.SpecifyKind(Convert.ToDateTime(reader["TransactionDate"]), DateTimeKind.Utc) // Or DateTimeKind.Local based on your need
                                            : (DateTime?)null
                                        ,

                                        ReturnDueDate = reader["ReturnDueDate"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["ReturnDueDate"])
                                    : (DateTime?)null,

                                        // Adjusted for status mapping
                                        Status = reader["CurrentStatus"].ToString()  // Assuming "CurrentStatus" is the status of the transaction

                                    };

                                    libraryTransactionDTOList.Add(libraryTransactionDTO);
                                }
                            }
                        }
                    }
                }
            }

            return libraryTransactionDTOList;
        }




    }
}