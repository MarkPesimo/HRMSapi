using HRMS_API.Helper;
using HRMS.REPO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static HRModel.ViewModel.Employees.Movement.Movement_model;
using HRMS_API.Repository;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class MovementController : ApiController
    {
        private MovementRepository _movementrepository { get; set; }

        public MovementController()
        {
            if (_movementrepository == null) { _movementrepository = new MovementRepository(); }
        }

        [Route("api/Movement/MovementCategory")]
        [HttpGet]
        public HttpResponseMessage MovementCategory()
        {
            try
            {
                List<MovementCategory_model> results = _movementrepository.GetMovementCategory();

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Movement/GetMovementMonitoring/{Keyword}/{ByClient}/{ClientID}/{ByCategory}/{CategoryId}/{ByDate}/{DateFrom}/{DateTo}/{PageNo}/{PageSize}/{CompanyID}")]
        [HttpGet]
        public HttpResponseMessage GetMovementMonitoring(string Keyword, bool ByClient, int ClientID, bool ByCategory, int CategoryId, bool ByDate, DateTime DateFrom, DateTime DateTo, int PageNo, int PageSize, int CompanyID)
        {
            try
            {
                List<MovementMonitoring_model> _model = _movementrepository.GetMovementMonitoring(Keyword, ByClient, ClientID, ByCategory, CategoryId, ByDate, DateFrom, DateTo, PageNo, PageSize, CompanyID);
                if (_model != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Movement/GetMovementRecord/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetMovementRecord(int Id)
        {
            try
            {
                MovementRecord_model _obj = _movementrepository.GetMovementRecord(Id);

                return _obj != null
                    ? Request.CreateResponse(HttpStatusCode.OK, _obj)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Movement/ManageMovementRecord")]
        [HttpPost]
        public HttpResponseMessage ManageMovementRecord([FromBody] MovementRecord_model model)
        {
            try
            {
                int result = _movementrepository.ManageMovementRecord(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}