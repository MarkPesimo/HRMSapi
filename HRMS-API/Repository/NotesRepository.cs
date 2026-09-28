using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Employees.Notes.EmployeeNotes_model;

namespace HRMS_API.Repository
{
    public class NotesRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }

        public NotesRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }

        public List<NotesMonitoring_model> GetEmployeeNotes(NotesFilter_model _filter)
        {
            return (from x in _conn.USP_H_GET_ACTIVE_EMPLOYEE_NOTES(
                _filter.ByDate,
                _filter.DateFrom,
                _filter.DateTo,
                _filter.ByStatus,
                _filter.Status,
                _filter.UserId)
                    select x
              ).AsEnumerable()
              .Select(d => new NotesMonitoring_model()
              {
                  Id = int.Parse(d.id.ToString()),
                  DateRequested = DateTime.Parse(d.date_requested.ToString()),
                  RequestedBy = d.requested_by,
                  EmpId = int.Parse(d.emp_id.ToString()),
                  EmployeeName = d.employee_name,
                  EmployeeGUID = d.employee_guid,
                  NoteType = d.note_type,
                  Remarks = d.remarks,
                  Status = d.status,
                  DateCompleted = d.date_completed.ToString() == "" || d.date_completed.ToString() == "-" ? "" : DateTime.Parse(d.date_completed.ToString()).ToShortDateString(),
                  CompletedBy = d.completed_by,
                  EmploymentType = d.employment_type
              }).ToList();
        }

        public Notes_model GetNote(int _id)
        {
            return (from x in _conn.USP_H_GET_EMPLOYEE_NOTE(_id)
                    select x
             ).AsEnumerable()
             .Select(d => new Notes_model()
             {
                 Id = int.Parse(d.id.ToString()),
                 DateRequested = DateTime.Parse(d.date_requested.ToString()),
                 RequestedBy = d.requested_by,
                 EmployeeName = d.employee_name,
                 EmployeeID = d.emp_id ?? 0,
                 CandId = d.cand_id ?? 0,
                 NoteType = d.note_type,
                 Remarks = d.remarks,
                 Status = d.status,
                 DateCompleted = d.date_completed.ToString() == "" ? "" : d.date_completed.ToString(),
                 CompletedBy = d.completed_by,
                 DocumentExt = d.document_ext
             }).SingleOrDefault();
        }

        public Notes_model GetEmployeeNote(int _id)
        {
            return (from x in _conn.USP_H_GET_EMPLOYEE_NOTE_COOR_PORTAL(_id)
                    select x
             ).AsEnumerable()
             .Select(d => new Notes_model()
             {
                 Id = int.Parse(d.id.ToString()),
                 DateRequested = DateTime.Parse(d.date_requested.ToString()),
                 RequestedBy = d.requested_by,
                 EmployeeName = "-",
                 NoteType = "New Employee",
                 Remarks = d.remarks,
                 Status = d.status,
                 DateCompleted = d.date_completed,
                 CompletedBy = d.completed_by,
                 DocumentExt = d.document_name,
             }).SingleOrDefault();
        }

        public PortalSubmittedDocument GetPortalSubmittedDocument(int _id)
        {
            return (from x in _conn.USP_H_GET_SUBMITTED_REQUIRED_DOCUMENT(_id)
                    select x
             ).AsEnumerable()
             .Select(d => new PortalSubmittedDocument()
             {
                 id = int.Parse(d.id.ToString()),
                 CandidateId = d.candidate_id,
                 CandidateName = d.CandName,
                 RequiredDocId = d.required_doc_id,
                 Document = d.Description,
                 DocId = d.doc_id,
                 FileType = d.file_type
             }).SingleOrDefault();
        }

        public int ManageDocument(AddCandidateDocuments _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_H_ADD_CANDIDATE_DOCUMENT(
                _model.EmpID,
                _model.UserAddedID,
                _model.DocId,
                _model.Remarks,
                _model.FileLocation,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }

        public int ManagePortalDocumentSubmitted(PortalSubmittedDocument _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_P_MANAGE_SUBMIT_REQUIRED_DOCUMENT(_model.id,
                _model.CandidateId,
                _model.RequiredDocId,
                _model.DocId,
                _model.FileType,
                _model.Mode,
                _model.UserId,
                _return_value);

            return Convert.ToInt32(_return_value.Value);
        }

        public int ManageEmployeeNote(Notes_model _model) // int _id, DateTime _datecompleted, int _userid, int _mode)
        {
            int? _ReturnId = 0;
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));


            _conn.USP_H_MANAGE_ACTIVE_EMPLOYEE_NOTES(_model.Id,
                0,
                DateTime.Now,
                0,
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                DateTime.Parse( _model.DateCompleted),
                _model.UserId,
                _model.Mode,
                _return_value);

            return int.Parse(_ReturnId.Value.ToString());
        }

        public int ManageCoorNewEmployeeNote(CoorNewPortal _model )
        {
            
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));
            
            _conn.USP_C_MANAGE_COOR_NEW_EMPLOYEE(_model.Id,
                _model.Userid,
                _model.Remarks,
                "",
                _model.Mode,
                _return_value);

            return int.Parse(_return_value.Value.ToString());
        }


    }
}