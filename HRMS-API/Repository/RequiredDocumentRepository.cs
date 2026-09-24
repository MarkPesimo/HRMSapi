using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Employees.Document.RequiredDocument_model;

namespace HRMS_API.Repository
{
    public class RequiredDocumentRepository
    {
        public static apwdbEntities _conn { get; set; }

        public RequiredDocumentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }          
        }

        public List<Monitoring_model> GetMonitoring(int _clientid)
        {
            return (from x in _conn.USP_H_GET_ES_CLIENT_EMPLOYEES(_clientid)
                    select x
              ).AsEnumerable()
              .Select(d => new Monitoring_model()
              {
                  EmpId = int.Parse(d.emp_id.ToString()),
                  ClientName = d.client_name,
                  EmpNo = d.emp_no,
                  EmployeeName = d.employee_name,
                  Remarks = d.remarks,
                  MissingDocument = d.req_document,
                  DateHired = d.date_hired.Value.ToShortDateString(),
                  ContractEnd = d.contract_end.Value.ToShortDateString(),
                  ContractStart = d.contract_start.Value.ToShortDateString(),
                  Age =int.Parse(  d.age.ToString())
              }).ToList();
        }


        public List<RequiredDocument_list_model> GetRequiredDocuments()
        {
            return (from x in _conn.Required_Document
                    select x
              ).AsEnumerable()
              .Select(d => new RequiredDocument_list_model()
              {
                  Id = int.Parse(d.id.ToString()),
                  Description = d.description,
                  WithSimilarDocument = d.doc_cnt,
                  IsRequired = d.required ? "Yes" : "No",
                  CreatedBy = d.SYS_USER.username,
                  DateCreated = d.date_created.ToShortDateString()
              }).ToList();
        }

        public RequiredDocument GetRequiredDocument(int _id)
        {
            return (from x in _conn.Required_Document
                    where x.id == _id
                    select x
              ).AsEnumerable()
              .Select(d => new RequiredDocument()
              {
                  Id = int.Parse(d.id.ToString()),
                  Description = d.description,
                  WithSimilarDocument = d.doc_cnt,
                  IsRequired = d.required ,
                  UserId = d.user_id
              }).SingleOrDefault();
        }

        public int ManageRequiredDocument(RequiredDocument _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_H_MANAGE_REQUIRED_DOCUMENT(
                _model.Id,
                _model.Description,
                _model.WithSimilarDocument,
                _model.IsRequired,
                _model.UserId,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }


        public List<SimilarDocument> GetSimilarDocuments(int _id)
        {
            return (from x in _conn.Required_Document_details
                    where x.req_id == _id
                    select x
              ).AsEnumerable()
              .Select(d => new SimilarDocument()
              {
                  Id = int.Parse(d.id.ToString()),
                  RequiredDocumentId = d.req_id,
                  DocumentId = d.doc_id,
                  DocumentName = d.Document.Description,
                  UserId = d.user_id,
                  CreatedBy = d.SYS_USER.username,
                  DateCreated = d.date_created
              }).ToList();
        }

        public SimilarDocument GetSimilarDocument(int _id)
        {
            return (from x in _conn.Required_Document_details
                    where x.id == _id
                    select x
              ).AsEnumerable()
              .Select(d => new SimilarDocument()
              {
                  Id = int.Parse(d.id.ToString()),
                  RequiredDocumentId = d.req_id,
                  DocumentId = d.doc_id,
                  DocumentName = d.Document.Description,
                  UserId = d.user_id,
                  CreatedBy = d.SYS_USER.username,
                  DateCreated = d.date_created
              }).SingleOrDefault();
        }

        public int ManageSimilarDocument(SimilarDocument _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_H_MANAGE_REQUIRED_DOCUMENT_DETAIL(
                _model.Id,
                _model.RequiredDocumentId,
                _model.DocumentId,
                _model.UserId,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
    }
}