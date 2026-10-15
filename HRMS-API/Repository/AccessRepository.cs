using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Global.AccessModel;
using static HRModel.ViewModel.Global.MasterFile;
using static HRModel.ViewModel.Global.PortalAccount_model;

namespace HRMS_API.Repository
{
    public class AccessRepository
    {
        private accessEntities _accessconn { get; set; }
        //private GlobalRepository _globalrepository { get; set; }
        //private UserRepository _userrepository { get; set; }

        public AccessRepository()
        {
            if (_accessconn == null) { _accessconn = new accessEntities(); }          
        }

        public SYS_Module GetModule(string _modulename, int _appid, int _moduletypeid)
        {
            return (from d in _accessconn.SYS_Module
                    where d.MODULE_NAME == _modulename &&
                        d.MODULE_TYPE == _moduletypeid &&
                        d.MODULE_APP == _appid
                    select d).SingleOrDefault();
        }


        public bool ModuleAccess(ModuleAccess_model _model )
        {
            if (_model.UserType.ToUpper() == "ADMIN") { return true; }
            else
            {
                System.Data.Entity.Core.Objects.ObjectParameter _access_value = new System.Data.Entity.Core.Objects.ObjectParameter("ACCESS", typeof(int));
                System.Data.Entity.Core.Objects.ObjectParameter _active_value = new System.Data.Entity.Core.Objects.ObjectParameter("ACTIVE", typeof(int));

                _accessconn.SP_MODULE_ACCESS(
                    _model.ModuleName,
                    _model.ModuleTypeId,
                    _model.UserId,
                    _model.AppId,
                    _access_value,
                    _active_value);

                if (Convert.ToInt32(_active_value.Value) == 1)
                {
                    if (Convert.ToBoolean(_access_value.Value)) { return true; }
                    else { return false; }
                }
                else { return true; }
            }
        }

        public bool FunctionAccess(FunctionAccess_model _model)
        {
            if (_model.UserType.ToUpper() == "ADMIN") { return true; }
            else {
                System.Data.Entity.Core.Objects.ObjectParameter _access_value = new System.Data.Entity.Core.Objects.ObjectParameter("ACCESS", typeof(int));

                _model.ModuleTypeId = GetModule(_model.ModuleName, _model.AppId, _model.ModuleTypeId).MODULEID;

                _accessconn.SP_FUNCTION_ACCESS(
                    _model.ModuleName,
                    _model.FunctionAction,
                    _model.AppName,
                    _model.UserId,
                    _access_value);

                if (Convert.ToBoolean(_access_value.Value)) { return true; }
                else { return false; }
            }
        }

        public List<ModuleType_model> GetModuleTypes()
        {
            return (from d in _accessconn.sys_module_type
                    orderby d.id
                    select d).AsEnumerable()
                  .Select(x => new ModuleType_model()
                  {
                      Id = x.id,
                      ModuleType = x.module_type
                  }).ToList();
        }

        public List<AvailableModule_model> GetAvailableModules(int _userid, int _moduletypeid, int _applicationid)
        {
            return (from d in _accessconn.USP_S_GET_AVAILABLE_MODULES(_userid, _moduletypeid, _applicationid)
                    orderby d.id
                    select d).AsEnumerable()
                  .Select(x => new AvailableModule_model()
                  {
                      Id = int.Parse( x.id.ToString()),
                      ModuleName = x.module_name
                  }).ToList();
        }

        public List<AccessableModule_model> GetAccessableModules(int _userid, int _moduletypeid, int _applicationid)
        {
            return (from d in _accessconn.USP_S_GET_ASSIGNED_MODULES(_userid, _moduletypeid, _applicationid)
                    orderby d.id
                    select d).AsEnumerable()
                  .Select(x => new AccessableModule_model()
                  {
                      Id = int.Parse(x.id.ToString()),
                      ModuleId = int.Parse( x.module_id.ToString()),
                      ModuleName = x.module_name,
                      AddAccess = bool.Parse( x.access_add.ToString()),
                      EditAccess = bool.Parse(x.access_edit.ToString()),
                      DeleteAccess = bool.Parse(x.access_delete.ToString()),
                      PrintAccess = bool.Parse(x.access_print.ToString()),
                      PostAccess = bool.Parse(x.access_post.ToString()),
                      UnpostAccess = bool.Parse(x.access_unpost.ToString()),
                      CancelAccess = bool.Parse(x.access_cancel.ToString()),
                      ActivateAccess = bool.Parse(x.access_activate.ToString()),
                      DeactivateAccess = bool.Parse(x.access_deactivate.ToString()),
                      ProvidedBy = x.provided_by,
                      DateProvided = DateTime.Parse( x.date_provided.ToString())

                  }).ToList();
        }

        public bool CopyAccess(int _fromuserid, int _touserid)
        {
            try
            {
                _accessconn.SP_COPY_ACCESS(_fromuserid, _touserid);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int ManageAccessRights(AccessableModule_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _accessconn.USP_G_MANAGE_ACCESS_RIGHTS(
                _model.ModuleId,
                _model.UserId,
                _model.AddAccess,
                _model.EditAccess,
                _model.DeleteAccess,
                _model.PrintAccess,
                _model.PostAccess,
                _model.CancelAccess,
                true,
                true,
                _model.UnpostAccess,
                _model.UserId,
                _model.Id,
                _model.Mode,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }

        //===========================CLIENT PORTAL ACCESS=======================================
        public List<ClientPortalAccess_model> GetClientPortalAccess(int _clientid, string _viewtype)
        {
            return (from x in _accessconn.USP_G_GET_CLIENT_MODULES(_clientid, _viewtype)
                    select x
             ).AsEnumerable()
             .Select(d => new ClientPortalAccess_model()
             {
                 Id = int.Parse(d.id.ToString()),
                 ModuleId = int.Parse(d.module_id.ToString()),
                 ModuleName = d.module_name,
                 ProvidedBy = d.provided_by,
                 DateProvided = d.date_provided.ToString()
             }).ToList();
        }

        public int ManageClientPortalAccess(ClientPortalAccess_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _accessconn.USP_G_MAANGE_PORTAL_ACCESS(
                _model.Id,
                _model.ClientId,
                _model.ModuleName,
                _model.ModuleId,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
        //===========================CLIENT PORTAL ACCESS=======================================
    }
}