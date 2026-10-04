using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS_API.Repository.Employee
{
    public class EmployeeDocumentRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public EmployeeDocumentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public EmployeeDocumentModel GetDocument(int _id)
        {
            return ( from d in _conn.REC_CANDIDATE_DOCUMENT
                     where d.id == _id
                     select d).AsEnumerable()
                     .Select(x => new EmployeeDocumentModel()
                    {
                        Id   = x.id,
                        CandidateId = x.candidate_id,
                        DocumentId = x.doc_id,
                        DocumentName = x.Document.Description,
                        ReferenceNo = x.DocNo,
                        DateIssued = x.date_added,
                        FileLocation = x.file_location,
                    }).SingleOrDefault();
        }

        public List<EmployeeDocumentViewModel> GetDocuments(int _empid)
        {
            List<EmployeeDocumentViewModel> _obj = new List<EmployeeDocumentViewModel>();

            _obj = (from cd in _conn.REC_CANDIDATE_DOCUMENT
                    join d in _conn.Documents on cd.doc_id equals d.DocId
                    join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on cd.candidate_id equals l.candidate_id
                    where l.emp_id == _empid
                    select cd).AsEnumerable()
                  .Select(x => new EmployeeDocumentViewModel()
                  {
                      DocumentName = x.Document.Description,
                      DocumentType = "",
                      ReferenceNo = x.DocNo,
                      DateIssued = x.date_issued.HasValue ? x.date_issued.Value.ToShortDateString() : "",
                      FileLocation = x.file_location
                  }).ToList();

            return _obj;
        }

        public int Manage(EmployeeDocumentModel _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_I_MANAGE_DOCUMENT(
                _model.Mode,
                _model.Id,
                _model.CandidateId,
                _model.ReferenceNo,
                _model.DateIssued,
                _model.FileLocation,
                _model.Remarks,
                _model.UserId,
                _model.DocumentId,                
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
    }
}