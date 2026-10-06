using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientDocumentRepository
    {
        private static apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientDocumentRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<Document_vw_model> GetDocumentDropdown()
        {
            return (from x in _conn.Documents
                    select x
            ).AsEnumerable()
            .Select(d => new Document_vw_model()
            {
                DocId = d.DocId,
                Description = d.Description,
                DocumentClass = d.Document_class,
                Status = d.status.ToString(),
                CreatedBy = d.SYS_USER != null ? d.SYS_USER.username : d.user_id.ToString(),
                DateCreated = d.date_created != null ? Convert.ToDateTime(d.date_created).ToShortDateString() : string.Empty,
                AccessibleOutside = d.accessible_outside,
                CountryId = d.country_id
            }).ToList();
        }

        public List<ClientDocument_vw_model> GetList(int _clientid)
        {
            return (from x in _conn.REC_CLIENT_DOCUMENTS
                    where x.client_id == _clientid
                    select x
            ).AsEnumerable()
            .Select(d => new ClientDocument_vw_model()
            {
                Id = int.Parse(d.id.ToString()),
                DocumentType = d.Document.Description,
                DateIssued = d.date_issued.ToString(),
                ExpiryDate = d.date_expired.ToString(),
                Remarks = d.remarks,
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created.ToShortDateString()
            }).ToList();
        }

        public ClientDocument_model Get(int _id)
        {
            return (from x in _conn.REC_CLIENT_DOCUMENTS
                    where x.id == _id
                    select x
            ).AsEnumerable()
            .Select(d => new ClientDocument_model()
            {
                Id = int.Parse(d.id.ToString()),
                ClientId = d.client_id,
                DocumentId = d.doc_id,
                DocumentName = d.Document.Description,
                DateIssued = d.date_issued,
                ExpiryDate = d.date_expired,
                UserId = d.user_created,
                CreatedBy = d.SYS_USER.username,
                DateCreated = d.date_created,
                FileLocation = d.file_location,
                FileExtension = d.file_extension,
                IsSigned = d.is_signed,
                IsOriginal = d.is_withoriginal,
                IsNotarized = d.is_notarized,
                Remarks = d.remarks
            }).SingleOrDefault();
        }

        public int Manage(ClientDocument_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_I_MANAGE_CLIENT_DOCUMENTS(
                _model.Mode,
                _model.Id,
                _model.ClientId,
                _model.DocumentId,
                _model.DateIssued,
                _model.ExpiryDate,
                _model.FileLocation,
                _model.Remarks,
                _model.FileExtension,
                _model.UserId,
                _return_value,
                _model.IsSigned,
                _model.IsOriginal,
                _model.IsNotarized
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
    }
}