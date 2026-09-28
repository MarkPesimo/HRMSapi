using HRModel.ViewModel.Global;
using HRMS.DB;
//using HRMS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Global.PortalAccount_model;

namespace HRMS_API.Repository
{
    public class AccountRepository
    {
        private apwdbEntities _conn { get; set; }

        public AccountRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
           
        }

        public LoginUser_model Get(string _username, string _password, int _appmoduleid)
        {
            LoginUser_model _obj = new LoginUser_model();

         
                _obj = (from d in _conn.SYS_USER
                        where d.username == _username &&
                            d.password == _password &&
                            d.appmodule_id == _appmoduleid
                        select d
                                         ).AsEnumerable()
                         .Select(x => new LoginUser_model()

                         {
                             UserId = x.id,
                             CandidateId = 0,
                             Username = x.username,
                             Usertype = x.user_type,
                             EmailAddress = x.email_address,
                             AppModuleId = x.appmodule_id,
                             EmployeeName = x.emp_name,
                             Password = x.password,
                             Status = x.status,
                             Guid = "",
                             CandidateStatus = "",
                             ClientId = 0,
                             Companyid = GetIsearchUserCompany(x.id),
                             CompanyWebsite = GetIsearchUserCompanyInfo(x.id, "Website"),
                             CompanyLogo = GetIsearchUserCompanyInfo(x.id, "Logo"),
                             CountryId = int.Parse(GetIsearchUserCompanyInfo(x.id, "Country"))
                         }).SingleOrDefault();
            

            return _obj;
        }

        public LoginUser_model Get(string _username, int _appmoduleid)
        {
            LoginUser_model _obj = new LoginUser_model();
           
            _obj = (from d in _conn.SYS_USER
                    where d.username == _username
                        && d.appmodule_id == _appmoduleid
                    select new LoginUser_model
                    {
                        UserId = d.id,
                        Username = d.username,
                        Usertype = d.user_type,
                        EmailAddress = d.email_address,
                        AppModuleId = d.appmodule_id,
                        EmployeeName = d.emp_name,
                        Password = d.password,
                        Guid = "",
                    }).SingleOrDefault();

            if (_obj != null)
            {
                _obj.GroupId = GetUserGroup(_obj.UserId).group_id;
                _obj.UserClass = GetUserGroup(_obj.UserId).user_type;
                _obj.Companyid = GetIsearchUserCompany(_obj.UserId);
                _obj.CountryId = GetIsearchUserCountry(_obj.UserId);
            }
           
            return _obj;
        }

        public SYS_USER_GROUP_DET GetUserGroup(int _userid)
        {
            return (from d in _conn.SYS_USER_GROUP_DET
                    where d.user_id == _userid
                    select d).SingleOrDefault();
        }

        int GetIsearchUserCompany(int _userid)
        {
            SYS_USER_GROUP_DET _gd = (from d in _conn.SYS_USER_GROUP_DET where d.user_id == _userid select d).SingleOrDefault();
            if (_gd != null)
            {
                return _gd.SYS_USER_GROUP.company_id;

            }

            return 0;
        }

        int GetIsearchUserCountry(int _userid)
        {
            SYS_USER_GROUP_DET _gd = (from d in _conn.SYS_USER_GROUP_DET where d.user_id == _userid select d).SingleOrDefault();
            if (_gd != null)
            {
                return _gd.SYS_USER_GROUP.sys_company.country_id;

            }

            return 0;
        }

        string GetIsearchUserCompanyInfo(int _userid, string _getwhat)
        {
            SYS_USER_GROUP_DET _gd = (from d in _conn.SYS_USER_GROUP_DET where d.user_id == _userid select d).SingleOrDefault();
            if (_gd != null)
            {
                if (_getwhat == "Website") { return _gd.SYS_USER_GROUP.sys_company.CompanyWebsite; }
                else if (_getwhat == "Logo") { return _gd.SYS_USER_GROUP.sys_company.CompanyLogo; }
                if (_getwhat == "Country") { return _gd.SYS_USER_GROUP.sys_company.country_id.ToString(); }
            }

            return "";
        }


        public void LogLogin(int _userid)
        {
            REC_USER_LOG _log = new REC_USER_LOG
            {
                id = 0,
                user_id = _userid,
                date_created = DateTime.Now
            };

            _conn.REC_USER_LOG.Add(_log);
            _conn.SaveChanges();
        }

        public List<NotYetRegisteredEmployee_model> GetEmployeesWithNoPortalAccount(int _clientid)
        {
            return (from x in _conn.USP_B_GENERATE_NOT_YET_REGISTERED_EMPLOYEE(_clientid)
                    select x
             ).AsEnumerable()
             .Select(d => new NotYetRegisteredEmployee_model()
             {
                EmpId = d.emp_id,
                EmpNo = d.emp_no,
                EmployeeName = d.EmployeeName,
                Lastname = d.Lastname,
                Firstname = d.Firstname,
                Middlename = d.Middlename,
                EmailAddress = d.EmailAddress
             }).ToList();
        }

        public bool RegisterPortalAccount(RegisterPortalAccount_model _model)
        {
            try
            {
                _conn.USP_E_MANAGE_ACCOUNT(
                   _model.Username,
                   _model.Password,
                   _model.HashPassword,
                   _model.EmpId,
                   _model.EmailAddress,
                   _model.UserId);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
    }
}