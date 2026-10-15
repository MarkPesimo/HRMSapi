using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Employees.Movement.Movement_model;

namespace HRMS_API.Repository
{
    public class MovementRepository
    {
        private apwdbEntities _conn { get; set; }
        private UserRepository _userrepository { get; set; }

        public MovementRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_userrepository == null) { _userrepository = new UserRepository(); }
        }

        public List<MovementCategory_model> GetMovementCategory()
        {
            return (from d in _conn.Personnel_Movement_Category
                    orderby d.Category_Description
                    select d).AsEnumerable()
               .Select(x => new MovementCategory_model()
               {
                   Id               = int.Parse(x.PM_CategoryID.ToString()),
                   Category         = x.Category_Description
               }).ToList();
        }

        public List<MovementMonitoring_model> GetMovementMonitoring(string Keyword, bool ByClient, int ClientID, bool ByCategory, int CategoryId, bool ByDate, DateTime DateFrom, DateTime DateTo, int PageNo, int PageSize, int CompanyID)
        {
            if (Keyword == "NULL") { Keyword = ""; }
            return (from d in _conn.USP_H_GET_PERSONNEL_MOVEMENT_MONITORING(Keyword, ByClient, ClientID, ByCategory, CategoryId, ByDate, DateFrom, DateTo, PageNo, PageSize, CompanyID)
                    select d).AsEnumerable()
                  .Select(x => new MovementMonitoring_model()
                  {
                      Id                = int.Parse(x.id.ToString()),
                      EmpId             = int.Parse(x.EmpID.ToString()),
                      EmployeeName      = x.EmployeeName,
                      ClientName        = x.ClientName,
                      Department        = x.Department,
                      Branch            = x.Branch,
                      Position          = x.Position,
                      PayType           = x.Paytype,
                      EffectiveDate     = DateTime.Parse(x.EffectiveDate.Value.ToShortDateString()),
                      SalaryChangeDate  = DateTime.Parse(x.SalaryChangeDate.Value.ToShortDateString())
                  }).ToList();
        }

        public MovementRecord_model GetMovementRecord(int _id) 
        {
            MovementRecord_model _obj = new MovementRecord_model();

            _obj = (from d in _conn.Employee_Personnel_Movement
                    join mc in _conn.Personnel_Movement_Category on d.PM_CategoryID equals mc.PM_CategoryID
                    where d.ID == _id
                    select d).AsEnumerable()
                  .Select(x => new MovementRecord_model()
                  {
                      Id                    = x.ID,
                      EmpId                 = x.EmpID,
                      EmployeeName          = x.Employee.Lastname + ", " + x.Employee.Firstname + " " + x.Employee.Middlename,
                      CategoryId            = x.PM_CategoryID,
                      Category              = x.Personnel_Movement_Category.Category_Description,
                      ClientIdFrom          = x.ClientID_From,
                      ClientFrom            = x.REC_CLIENT.client_name,
                      ClientIdTo            = x.ClientID_To,
                      ClientTo              = x.REC_CLIENT1.client_name,
                      BranchIdFrom          = x.BranchID_From,
                      BranchFrom            = x.Branch.Branch_Desc,
                      BranchIdTo            = x.BranchID_To,
                      BranchTo              = x.Branch1.Branch_Desc,
                      DepartmentIdFrom      = x.DepartmentID_From,
                      DepartmentFrom        = x.Department.Dept_Name,
                      DepartmentIdTo        = x.DepartmentID_To,
                      DepartmentTo          = x.Department1.Dept_Name,
                      EmployeeTypeIdFrom    = x.EmpTypeID_From,
                      EmployeeTypeFrom      = x.EmployeeType.EmpType_Desc,
                      EmployeeTypeIdTo      = x.EmpTypeID_To,
                      EmployeeTypeTo        = x.EmployeeType1.EmpType_Desc,
                      PositionFrom          = x.Position_From,
                      PositionTo            = x.Position_To,
                      EmployeeRankIdFrom    = x.EmpRankID_From,
                      EmployeeRankFrom      = x.EmployeeRank.EmployeeRank1,
                      EmployeeRankIdTo      = x.EmpRankID_To,
                      EmployeeRankTo        = x.EmployeeRank1.EmployeeRank1,
                      PayTypeFrom           = x.PayType_From,
                      PayTypeTo             = x.PayType_To,
                      MonthlySalaryFrom     = x.MonthlySalary_From,
                      MonthlySalaryTo       = x.MonthlySalary_To,
                      AllowanceFrom         = x.Allowance_From,
                      AllowanceTo           = x.Allowance_To,
                      EffectiveDateFrom     = x.EffectiveDate_From,
                      EffectiveDateTo       = x.EffectiveDate_To,
                      SalaryChangeDate      = x.Salary_Change_Date,
                      ProcessDate           = x.process_date.HasValue ? x.process_date.Value.ToString() : "",
                      ProcessStatus         = bool.Parse(x.process_status.ToString()),
                      UserCreatedId         = int.Parse(x.User_Created.ToString()),
                      CreatedBy             = _userrepository.GetUser(int.Parse(x.User_Created.ToString())).UserName,
                      DateCreated           = x.Date_Created.ToShortDateString(),
                      UserModifiedId        = x.User_Modified == 0 ? int.Parse(x.User_Modified.ToString()) : 0,
                      ModifiedBy            = x.User_Modified > 0 ? _userrepository.GetUser(int.Parse(x.User_Modified.ToString())).UserName : "",
                      DateModified          = x.Date_Modified.HasValue ? x.Date_Modified.Value.ToString() : ""
                  }).SingleOrDefault();

            return _obj;
        }

        public int ManageMovementRecord(MovementRecord_model _model)
        {
            try
            {
                string _return = "";

                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_H_MANAGE_EMPLOYEE_PERSONNEL_MOVEMENT( 
                    _model.Id,
                    _model.Mode,
                    _model.EmpId,
                    _model.CategoryId,
                    _model.ClientIdFrom,
                    _model.ClientIdTo,
                    _model.BranchIdFrom,
                    _model.BranchIdTo,
                    _model.DepartmentIdFrom,
                    _model.DepartmentIdTo,
                    _model.EmployeeTypeIdFrom,
                    _model.EmployeeTypeIdTo,
                    _model.EmployeeRankIdFrom,
                    _model.EmployeeRankIdTo,
                    _model.PositionFrom,
                    _model.PositionTo,
                    _model.PayTypeFrom,
                    _model.PayTypeTo,
                    _model.MonthlySalaryFrom,
                    _model.MonthlySalaryTo,
                    _model.AllowanceFrom,
                    _model.AllowanceTo,
                    _model.EffectiveDateFrom,
                    _model.EffectiveDateTo,
                    _model.SalaryChangeDate,
                    _model.UserCreatedId,
                    _return_value);

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