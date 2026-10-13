using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Global.AccessModel;
using static HRModel.ViewModel.Global.MasterFile;
using static HRModel.ViewModel.Global.PortalAccount_model;

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
                    FunctionAction = FunctionAction,
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


        [Route("api/Access/GetModuleTypes")]
        [HttpGet]
        public HttpResponseMessage GetModuleTypes()
        {
            try
            {
                List<ModuleType_model> _obj = _accessrepository.GetModuleTypes();
                return Request.CreateResponse(HttpStatusCode.OK, _obj);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }


        [Route("api/Access/GetAvailableModules/{UserId}/{ModuleTypeId}/{ApplicationId}")]
        [HttpGet]
        public HttpResponseMessage GetAvailableModules(int UserId, int ModuleTypeId, int ApplicationId)
        {
            try
            {
                List<AvailableModule_model> _obj = _accessrepository.GetAvailableModules(UserId, ModuleTypeId, ApplicationId);
                return Request.CreateResponse(HttpStatusCode.OK, _obj);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Access/GetAccessableModules/{UserId}/{ModuleTypeId}/{ApplicationId}")]
        [HttpGet]
        public HttpResponseMessage GetAccessableModules(int UserId, int ModuleTypeId, int ApplicationId)
        {
            try
            {
                List<AccessableModule_model> _obj = _accessrepository.GetAccessableModules(UserId, ModuleTypeId, ApplicationId);
                return Request.CreateResponse(HttpStatusCode.OK, _obj);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Access/CopyAccess/{FromUserId}/{ToUserId}")]
        [HttpPost]
        public HttpResponseMessage CopyAccess(int FromUserId, int ToUserId)
        {
            try
            {
                bool result = _accessrepository.CopyAccess(FromUserId, ToUserId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Access/ManageAccess")]
        [HttpPost]
        public HttpResponseMessage ManageSalaryRemarks([FromBody] AccessableModule_model model)
        {
            try
            {
                int result = _accessrepository.ManageAccessRights(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        //===============================================CLIENT PORTAL ACCESS================================================

        [Route("api/Access/GetClientPortalAccess/{ClientId}/{ViewType}")]
        [HttpGet]
        public HttpResponseMessage GetClientPortalAccess(int ClientId, string ViewType)
        {
            try
            {
                List<ClientPortalAccess_model> _obj = _accessrepository.GetClientPortalAccess(ClientId, ViewType);
                return Request.CreateResponse(HttpStatusCode.OK, _obj);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Access/ManageClientPortalAccess")]
        [HttpPost]
        public HttpResponseMessage ManageClientPortalAccess([FromBody] ClientPortalAccess_model _model)
        {
            try
            {
                int _result = _accessrepository.ManageClientPortalAccess(_model);
                if (_result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }
        //===============================================CLIENT PORTAL ACCESS================================================
    }
}
