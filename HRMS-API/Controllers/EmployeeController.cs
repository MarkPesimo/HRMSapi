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
        private EmployeeRepository employeerepository { get; set; }

        public EmployeeController()
        {
            if (employeerepository == null) { employeerepository = new EmployeeRepository(); }
        }

        [Route("api/Employee/GetEmployeeMonitoring")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeMonitoring()
        {
            try
            {
                //LoginUser_model _loginuser_model = accountrepository.Get(Username, AppModuleId);
                //if (_loginuser_model != null)
                //{
                return Request.CreateResponse(HttpStatusCode.OK, "Welcome");
                //}
                //else
                //{ return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }
    }
}
