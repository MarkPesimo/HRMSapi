using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}