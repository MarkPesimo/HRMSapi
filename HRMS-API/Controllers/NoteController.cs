using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static HRModel.ViewModel.Employees.Notes.EmployeeNotes_model;

namespace HRMS_API.Controllers
{
    public class NoteController : ApiController
    {
        private GlobalRepository _globalrepository { get; set; }
        private EmployeeRepository _employeerepository { get; set; }
        private NotesRepository _noterepository { get; set; }

        public NoteController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_employeerepository == null) { _employeerepository = new EmployeeRepository(); }
            if (_noterepository == null) { _noterepository = new NotesRepository(); }            
        }

        [Route("api/Notes/GetMonitoring/{ByDate}/{DateFrom}/{DateTo}/{ByStatus}/{Status}/{UserId}")]
        [HttpGet]
        public HttpResponseMessage GetMonitoring(bool ByDate, DateTime DateFrom, DateTime DateTo, bool ByStatus, bool Status, int UserId)
        {
            try
            {
                NotesFilter_model _filter = new NotesFilter_model
                {
                    ByDate = ByDate,
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    ByStatus = ByStatus,
                    Status = Status,
                    UserId = UserId
                };

                List<NotesMonitoring_model> _model = _noterepository.GetEmployeeNotes(_filter);
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/GetNote/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetNote(int Id)
        {
            try
            {
                Notes_model _model = _noterepository.GetNote(Id);
                if (_model != null ) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/GetEmployeeNote/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeNote(int Id)
        {
            try
            {
                Notes_model _model = _noterepository.GetEmployeeNote(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/GetPortalSubmittedDocument/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetPortalSubmittedDocument(int Id)
        {
            try
            {
                PortalSubmittedDocument _model = _noterepository.GetPortalSubmittedDocument(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/ManageDocument")]
        [HttpPost]
        public HttpResponseMessage ManageDocument([FromBody] AddCandidateDocuments model)
        {
            try
            {
                int result = _noterepository.ManageDocument(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/ManagePortalDocumentSubmitted")]
        [HttpPost]
        public HttpResponseMessage ManagePortalDocumentSubmitted([FromBody] PortalSubmittedDocument model)
        {
            try
            {
                int result = _noterepository.ManagePortalDocumentSubmitted(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/ManageEmployeeNote")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeNote([FromBody] Notes_model model)
        {
            try
            {
                int result = _noterepository.ManageEmployeeNote(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/ManageCoorNewEmployeeNote")]
        [HttpPost]
        public HttpResponseMessage ManageCoorNewEmployeeNote([FromBody] CoorNewPortal model)
        {
            try
            {
                int result = _noterepository.ManageCoorNewEmployeeNote(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Notes/GetFilePath/{type}")]
        [HttpGet]
        public HttpResponseMessage GetFilePath(string type)
        {
            try
            {
                string filePath = _globalrepository.GetFilePath(type);

                if (!string.IsNullOrEmpty(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, filePath);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No path configuration found for the specified type!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

    }
}
