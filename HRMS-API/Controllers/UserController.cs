using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Global.PortalAccount_model;
using static HRModel.ViewModel.Users.User_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class UserController : ApiController
    {
        private UserRepository _userrepository { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public UserController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }
        
        [Route("api/User/GetUsersByApp/{AppId}/{CompanyId}")]
        [HttpGet]
        public HttpResponseMessage GetUsersByApp(int AppId, int CompanyId)
        {
            try
            {
                List<UserModel> _model = _userrepository.GetUsersByApp(AppId, CompanyId);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/User/GetPortalUsers/{CompanyId}/{PageNumber}/{PageSize}/{Keyword}")]
        [HttpGet]
        public HttpResponseMessage GetPortalUsers(int CompanyId, int PageNumber, int PageSize, string Keyword)
        {
            try
            {
                PortalUser_vw__model _model = _userrepository.GetPortalUsersByCompany(CompanyId, PageNumber, PageSize, Keyword);
                return Request.CreateResponse(HttpStatusCode.OK, _model); 
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/User/GetPortalUser/{EmpId}")]
        [HttpGet]
        public HttpResponseMessage GetPortalUser(int EmpId)
        {
            try
            {
                PortalUser_model _model = _userrepository.GetPortalUser(EmpId);
                return Request.CreateResponse(HttpStatusCode.OK, _model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/User/ManagePortalUser")]
        [HttpPost]
        public HttpResponseMessage ManagePortalUser([FromBody] PortalUser_model model)
        {
            try
            {
                bool result = _userrepository.ManagePortalUser(model);
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}
