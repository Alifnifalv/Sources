using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Supports
{
    public class TicketDTO : BaseMasterDTO
    {
        public TicketDTO()
        {
            TicketProductSKUs = new List<TicketProductDTO>();
            Document = new DocumentViewDTO();
            TicketCommunications = new List<TicketCommunicationDTO>();
            TicketFeeDueMapDTOs = new List<TicketFeeDueMapDTO>();
        }

        [DataMember]
        public long TicketIID { get; set; }

        [DataMember]
        public string TicketNo { get; set; }

        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Description2 { get; set; }

        [DataMember]
        public int? Source { get; set; }

        [DataMember]
        public byte? PriorityID { get; set; }

        [DataMember]
        public byte? ActionID { get; set; }

        [DataMember]
        public byte? TicketStatusID { get; set; }

        [DataMember]
        public string TicketStatus { get; set; }

        [DataMember]
        public long? AssignedEmployeeID { get; set; }

        [DataMember]
        public string AssignedEmployeeName { get; set; }

        [DataMember]
        public long? CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public long? SupplierID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public DateTime? DueDateFrom { get; set; }

        [DataMember]
        public DateTime? DueDateTo { get; set; }

        [DataMember]
        public bool? IsSendCustomerNotification { get; set; }

        [DataMember]
        public bool? OldIsSendCustomerNotification { get; set; }

        [DataMember]
        public DocumentViewDTO Document { get; set; }

        [DataMember]
        public List<TicketProductDTO> TicketProductSKUs { get; set; }

        [DataMember]
        public TicketActionDetailMapsDTO TicketActionDetail { get; set; }

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public long? ReferenceID { get; set; }

        [DataMember]
        public string CustomerEmailID { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string FromDueDateString { get; set; }

        [DataMember]
        public string ToDueDateString { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public byte? ReferenceTypeID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public long? HeadID { get; set; }

        [DataMember]
        public List<TicketCommunicationDTO> TicketCommunications { get; set; }

        [DataMember]
        public List<TicketFeeDueMapDTO> TicketFeeDueMapDTOs { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public KeyValueDTO Parent { get; set; }

        [DataMember]
        public bool? IsSendMailToAssignedEmployee { get; set; }

        [DataMember]
        public string AssignedEmployeeEmailID { get; set; }

        [DataMember]
        public int? TicketTypeID { get; set; }

        [DataMember]
        public int? SupportCategoryID { get; set; }

        [DataMember]
        public string SupportCategoryName { get; set; }

        [DataMember]
        public int? SupportSubCategoryID { get; set; }

        [DataMember]
        public string SupportSubCategoryName { get; set; }

        [DataMember]
        public int? FacultyTypeID { get; set; }

        [DataMember]
        public byte? StudentSchoolID { get; set; }

        [DataMember]
        public int? StudentAcademicYearID { get; set; }

        [DataMember]
        public int? StudentClassID { get; set; }

        [DataMember]
        public int? StudentSectionID { get; set; }

    }
}