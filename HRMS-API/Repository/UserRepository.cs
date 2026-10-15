using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Global.PortalAccount_model;
using static HRModel.ViewModel.Users.User_model;

namespace HRMS_API.Repository
{
    public class UserRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public UserRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        //=============================SYSTEM USERS==============================================
        public UserModel GetUser(int _id)
        {
            return (from d in _conn.SYS_USER
                    where d.id == _id
                    select d).AsEnumerable()
           .Select(x => new UserModel()
           {
               UserId = int.Parse(x.id.ToString()),
               UserName = x.username,
               EmployeeName = x.emp_name,
               UserType = x.user_type
           }).SingleOrDefault();

        }

        public List<UserModel> GetUsersByApp(int _appid, int _companyid)
        {

            //join a in _conn.AreaLibraries on d.CityID equals a.AreaID

            return (from d in _conn.SYS_USER
                    join  gd in _conn.SYS_USER_GROUP_DET on  d.id equals gd.user_id
                    where d.appmodule_id == _appid
                        && d.status == true
                        && gd.SYS_USER_GROUP.company_id == _companyid
                    select d).AsEnumerable()
           .Select(x => new UserModel()
           {
               UserId = int.Parse(x.id.ToString()),
               UserName = x.username,
               EmployeeName = x.emp_name,
               UserType = x.user_type
           }).ToList();

        }
        //=============================SYSTEM USERS==============================================

        //=============================PORTAL USERS==============================================
        public PortalUser_vw__model GetPortalUsersByCompany(int _companyid, int _pagenumber, int _pagesize, string _keyword)
        {
            PortalUser_vw__model result = new PortalUser_vw__model
            {
                RecountCount = 0,
                PortalUsers = new List<PortalUser_model>()
            };

            using (var conn = _conn.Database.Connection)
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "USP_S_GET_PORTAL_USERS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param1 = cmd.CreateParameter();
                    param1.ParameterName = "@COMPANY_ID";
                    param1.Value = _companyid;
                    cmd.Parameters.Add(param1);

                    var param2 = cmd.CreateParameter();
                    param2.ParameterName = "@PAGENUMBER";
                    param2.Value = _pagenumber;
                    cmd.Parameters.Add(param2);

                    var param3 = cmd.CreateParameter();
                    param3.ParameterName = "@PAGESIZE";
                    param3.Value = _pagesize;
                    cmd.Parameters.Add(param3);

                    var param4 = cmd.CreateParameter();
                    param4.ParameterName = "@KEYWORD";
                    param4.Value = _keyword;
                    cmd.Parameters.Add(param4);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.RecountCount = reader["TotalRecord"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalRecord"]);
                          
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                result.PortalUsers.Add(new PortalUser_model
                                {
                                    Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"]),
                                    ClientName = reader["client_name"].ToString(),
                                    EmployeeName = reader["employee_name"].ToString(),
                                    Username = reader["username"].ToString(),
                                    EmailAddress = reader["email_address"].ToString(),
                                    UserStatus = reader["User_Status"].ToString(),
                                    DateRegistered = reader["Date_Registered"] == DBNull.Value ? "" : Convert.ToDateTime(reader["Date_Registered"]).ToShortDateString(),
                                });
                            }
                        }
                    }

                    
                }
            }

            return result;
        }

        public PortalUser_model GetPortalUser(int _empid)
        {
            return (from d in _conn.USP_S_GET_PORTAL_USER(_empid)
                    select d).AsEnumerable()
            .Select(x => new PortalUser_model()
            {
                Id = int.Parse( x.userid.ToString()),
                EmpId = int.Parse(x.empid.ToString()),
                Username = x.username,
                ClientName = x.clientname,
                EmployeeName = x.employeename,
                Password = "",
                EmailAddress = x.email_address,
                Status = bool.Parse( x.status.ToString()),
                UserStatus = x.user_status,
                DateRegistered =  x.date_registered.Value.ToShortDateString() 
            }).SingleOrDefault();
        }

        public bool ManagePortalUser(PortalUser_model _model)
        {
            try
            {
                _conn.USP_S_MANAGE_PORTAL_USER(_model.EmpId, 
                    _model.Username, 
                    _model.EmailAddress, 
                    _model.Mode, 
                    _model.UserId);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //=============================PORTAL USERS==============================================
    }
}