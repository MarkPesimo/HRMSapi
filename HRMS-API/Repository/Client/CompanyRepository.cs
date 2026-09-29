using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS_API.Repository.Client
{
    public class CompanyRepository
    {
        private  apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public CompanyRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public int GetCompanyId(string _guid)
        {
            return (from d in _conn.sys_company where d.guid == _guid select d.id).SingleOrDefault();
        }


    }
}