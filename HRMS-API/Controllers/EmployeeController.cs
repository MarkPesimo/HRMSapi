using HRModel.ViewModel.Employees;
using HRModel.ViewModel.Global;
using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class EmployeeController : ApiController
    {
        private EmployeeRepository Employeerepository { get; set; }

        public EmployeeController()
        {
            if (Employeerepository == null) { Employeerepository = new EmployeeRepository(); }
        }

        [Route("api/Employee/GetEmployeeMonitoring/{Keyword}/{ByClient}/{ClientID}/{PageNo}/{PageSize}/{CompanyID}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeMonitoring(string Keyword, bool ByClient, int ClientID, int PageNo, int PageSize, int CompanyID)
        {
            try
            {
                List<EmployeeMonitoringViewModel> _model =  Employeerepository.GetEmployeeMonitoring(Keyword, ByClient, ClientID, PageNo, PageSize, CompanyID);
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

        [Route("api/Employee/GetPersonalInfo/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetPersonalInfo(string GUID)
        {
            try
            {
                EmployeeProfile _model = Employeerepository.GetEmployeeProfile(GUID);
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

        [Route("api/Employee/GetEmployeeEducation/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeEducation(string GUID)
        {
            try
            {
                EmployeeEducation _model = Employeerepository.GetEmployeeEducation(GUID);
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
    }
}
