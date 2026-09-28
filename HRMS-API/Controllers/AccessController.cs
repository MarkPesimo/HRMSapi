using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Global.AccessModel;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class AccessController : ApiController
    {
        private AccessRepository _accessrepository { get; set; }

        public AccessController()
        {
            if (_accessrepository == null) { _accessrepository = new AccessRepository(); }
        }

        [Route("api/Access/ModuleAccess/{ModuleName}/{ModuleTypeId}/{UserId}/{AppId}")]
        [HttpGet]
        public HttpResponseMessage ModuleAccess(string ModuleName, int ModuleTypeId, int UserId, int AppId)
        {
            try
            {
                ModuleAccess_model _model = new ModuleAccess_model
                {
                    ModuleName = ModuleName,
                    ModuleTypeId = ModuleTypeId,
                    UserId = UserId,
                    AppId = AppId
                };

                bool _hasaccess = _accessrepository.ModuleAccess(_model);
                return Request.CreateResponse(HttpStatusCode.OK, _hasaccess);  
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Access/FunctionAccess/{ModuleName}/{FunctionAction}/{AppName}/{UserId}")]
        [HttpGet]
        public HttpResponseMessage FunctionAccess(string ModuleName, string FunctionAction, string AppName, int UserId)
        {
            try
            {
                FunctionAccess_model _model = new FunctionAccess_model
                {
                    ModuleName = ModuleName,
                    Action = FunctionAction,
                    AppName = AppName,
                    UserId = UserId 
                };

                bool _hasaccess = _accessrepository.FunctionAccess(_model);
                return Request.CreateResponse(HttpStatusCode.OK, _hasaccess);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}
