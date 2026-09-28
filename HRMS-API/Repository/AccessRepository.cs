using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Global.AccessModel;

namespace HRMS_API.Repository
{
    public class AccessRepository
    {
        private accessEntities _conn { get; set; }
        //private GlobalRepository _globalrepository { get; set; }
        //private UserRepository _userrepository { get; set; }

        public AccessRepository()
        {
            if (_conn == null) { _conn = new accessEntities(); }          
        }

        public SYS_Module GetModule(string _modulename, int _appid, int _moduletypeid)
        {
            return (from d in _conn.SYS_Module
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

                _conn.SP_MODULE_ACCESS(
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

                _conn.SP_FUNCTION_ACCESS(
                    _model.ModuleName,
                    _model.Action,
                    _model.AppName,
                    _model.UserId,
                    _access_value);

                if (Convert.ToBoolean(_access_value.Value)) { return true; }
                else { return false; }
            }
        }
    }
}