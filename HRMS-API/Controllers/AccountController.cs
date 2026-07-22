using HRModel.ViewModel.Global;
using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class AccountController : ApiController
    {
        private AccountRepository accountrepository { get; set; }

        public AccountController()
        {
            if (accountrepository == null) { accountrepository = new AccountRepository(); }
        }

        [Route("api/Account/Login")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginModel _model)
        {
            try
            {
                LoginUser_model _loginuser_model = accountrepository.Get(_model.UserName, _model.Password, _model.AppModuleId);
                if (_loginuser_model != null)
                {
                    accountrepository.LogLogin(_loginuser_model.UserId);
                    return Request.CreateResponse(HttpStatusCode.OK, _loginuser_model);
                }
                else
                { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Account/GetByUserName/{Username}/{AppModuleId}")]
        [HttpGet]
        public HttpResponseMessage Get(string Username, int AppModuleId)
        {
            try
            {
                LoginUser_model _loginuser_model = accountrepository.Get(Username, AppModuleId);
                if (_loginuser_model != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _loginuser_model);
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