using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Contract.EmployeeTransaction.ReactivationModel;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class ReactivationController : ApiController
    {
        private ReactivationRepository _reactivationrepository { get; set; }
        private GlobalRepository GlobalRepository { get; set; }

        public ReactivationController()
        {
            if (_reactivationrepository == null) { _reactivationrepository = new ReactivationRepository(); }
            if (GlobalRepository == null) { GlobalRepository = new GlobalRepository(); }
            if (EmployeeRepository == null) { EmployeeRepository = new EmployeeRepository(); }
        }

        [Route("api/Reactivation/GetMonitoring/{ByStatus}/{Status}/{Keyword}")]
        [HttpGet]
        public HttpResponseMessage GetMonitoring(bool   ByStatus , bool Status, string Keyword)
        {
            try
            {
                List<EmployeeReactivationMonitoring_model> _model = _reactivationrepository.GetMonitoring(ByStatus, Status, Keyword);

                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Reactivation/Manage")]
        [HttpGet]
        public HttpResponseMessage Manage([FromBody] EmployeeReactivation_model model)
        {
            try
            {
                _reactivationrepository.ManageContractReactivation(model);
                return Request.CreateResponse(HttpStatusCode.OK, "Contract transaction saved successfully.");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}
