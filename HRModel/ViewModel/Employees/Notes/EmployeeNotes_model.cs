using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees.Notes
{
    public class EmployeeNotes_model
    {
        public class NotesFilter_model
        {
            public bool ByDate { get; set; }
            public DateTime DateFrom { get; set; } 
            public DateTime DateTo { get; set; }
            public bool ByStatus { get; set; }
            public bool Status { get; set; }
            public int UserId { get; set; }
        }

        public class NotesMonitoring_model
        {         
            public int Id { get; set; }
            public DateTime DateRequested { get; set; }
            public string RequestedBy { get; set; }
            public int EmpId { get; set; }
            public string EmployeeName { get; set; }
            public string NoteType { get; set; }
            public string Remarks { get; set; }
            public string Status { get; set; }
            public string DateCompleted { get; set; }
            public string CompletedBy { get; set; }
            public string EmploymentType { get; set; }
            public string EmployeeGUID { get; set; }
        }

        public class Notes_model
        {
            public int Id { get; set; }
            public DateTime DateRequested { get; set; }
            public string RequestedBy { get; set; }
            public string EmployeeName { get; set; }
            public int EmployeeID { get; set; }
            public int CandId { get; set; }
            public string NoteType { get; set; }
            public string Remarks { get; set; }
            public string Status { get; set; }
            public string DateCompleted { get; set; }
            public string CompletedBy { get; set; }
            public string DocumentExt { get; set; }

            public int UserId { get; set; }
            public int Mode { get; set; }
        }

        public class AddCandidateDocuments
        {
            public int EmpID { get; set; }
            public int UserAddedID { get; set; }
            public int DocId { get; set; }
            public string Remarks { get; set; }
            public string FileLocation { get; set; }
        }

        public class PortalSubmittedDocument
        {
            public int id { get; set; }
            public int CandidateId { get; set; }
            public string CandidateName { get; set; }
            public int RequiredDocId { get; set; }
            public string Document { get; set; }
            public int DocId { get; set; }
            public string FileType { get; set; }

            public int UserId { get; set; }
            public int Mode { get; set; }
        }

        public class CoorNewPortal
        {
            public int Id { get; set; }
            public int Userid { get; set; }
            public string Remarks { get; set; }
            public string DocumentName { get; set; }
            public int Mode { get; set; }
        }
    }
}
