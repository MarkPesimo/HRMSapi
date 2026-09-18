using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Floating.EmployeeTransaction_model;

namespace HRMS_API.Repository
{
    public class TransactionRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private UserRepository _userrepository { get; set; }

        public TransactionRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }

        public int ManageEmployeeContractFloating(Separation _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                //_conn.USP_H_MANAGE_EMPLOYEE_TRANSACTION(_model.Id,
                //    0,
                //    0,
                //    DateTime.Now,
                //    DateTime.Now,
                //    DateTime.Now,
                //    _model.LoginUserId,
                //    "",
                //    0,
                //    false,
                //    0,
                //    "",
                //    DateTime.Now,
                //    0,
                //    0,
                //    _model.DateResignationSubmitted,
                //    _model.DateSeparated,
                //    _model.SeparatedById,
                //    _model.FloatingId,
                //    DateTime.Now,
                //    DateTime.Now,
                //    0, 0, 0, 0,
                //    _model.Mode,
                //    _return_value);

                

                _return = Convert.ToString(_return_value.Value);

                return int.Parse(_return.ToString());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { throw new Exception(ex.InnerException.Message); }

                throw new Exception(ex.Message);
            }
        }

    }
}