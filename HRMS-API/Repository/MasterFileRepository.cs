using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Global.MasterFile;
using static HRModel.ViewModel.Global.MasterFile.BankTypeModel;
using static HRModel.ViewModel.Global.MasterFile.DepartmentModel;
using static HRModel.ViewModel.Global.MasterFile.DocumentTypeModel;
using static HRModel.ViewModel.Global.MasterFile.EmployeeRankModel;
using static HRModel.ViewModel.Global.MasterFile.EmployeeTypeModel;
using static HRModel.ViewModel.Global.MasterFile.SalaryTypeModel;
using static HRModel.ViewModel.Global.MasterFile.ShiftModel;

namespace HRMS_API.Repository
{
    public class MasterFileRepository
    {
        //public static apwdbEntities _conn { get; set; } 

        public MasterFileRepository()
        {
           // if (_conn == null) { _conn = new apwdbEntities(); }
            //_conn = new apwdbEntities();
        }

        public class Department_repository
        {
            public apwdbEntities _conn { get; set; }

            public Department_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public List<Department_list_model> Get()
            {
                return (from x in _conn.Departments
                        select x
                ).AsEnumerable()
                .Select(d => new Department_list_model()
                {
                    Id = int.Parse(d.Dept_ID.ToString()),
                    Description = d.Dept_Name,
                    Status = d.Status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created.ToShortDateString()
                }).ToList();
            }

            public Department_model Get(int _id)
            {
                return (from x in _conn.Departments
                        where x.Dept_ID == _id 
                        select x
                ).AsEnumerable()
                .Select(d => new Department_model()
                {
                    Id = int.Parse(d.Dept_ID.ToString()),
                    Description = d.Dept_Name,
                    Status = d.Status,
                    UserId = d.UserID,
                    CreatedBy = d.SYS_USER != null ? d.SYS_USER.username : "",
                    DateCreated = d.date_created
                }).SingleOrDefault();
            }

            public int Manage(Department_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_MASTER_DEPARTMENT(
                    _model.Mode,
                    _model.Description,
                    _model.Id,
                    _model.UserId,
                    _return_value
                );

                return Convert.ToInt32(_return_value.Value);
            }
        }

        public class EmployeeType_repository
        {
            public apwdbEntities _conn { get; set; }

            public EmployeeType_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public List<EmployeeType_list_model> Get()
            {
                return (from x in _conn.EmployeeTypes
                        select x
                ).AsEnumerable()
                .Select(d => new EmployeeType_list_model()
                {
                    Id = int.Parse(d.EmpType_ID.ToString()),
                    Description = d.EmpType_Desc,
                    Status = d.status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created.ToShortDateString()
                }).ToList();
            }

            public EmployeeType_model Get(int _id)
            {
                return (from x in _conn.EmployeeTypes
                        where x.EmpType_ID == _id
                        select x
                ).AsEnumerable()
                .Select(d => new EmployeeType_model()
                {
                    Id = int.Parse(d.EmpType_ID.ToString()),
                    Description = d.EmpType_Desc,
                    Status = d.status,
                    UserId = d.UserID,
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created
                }).SingleOrDefault();
            }

            public int Manage(EmployeeType_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_MASTER_EMPLOYEETYPE(
                    _model.Mode,
                    _model.Description,
                    _model.Id,
                    _model.UserId,
                    _return_value
                );

                return Convert.ToInt32(_return_value.Value);
            }
        }

        public class EmployeeRank_repository
        {
            public apwdbEntities _conn { get; set; }

            public EmployeeRank_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public List<EmployeeRank_list_model> Get()
            {
                return (from x in _conn.EmployeeRanks
                        select x
                ).AsEnumerable()
                .Select(d => new EmployeeRank_list_model()
                {
                    Id = int.Parse(d.EmpRank_ID.ToString()),
                    Description = d.EmployeeRank1,
                    Status = d.Status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.Date_created.ToShortDateString()
                }).ToList();
            }

            public EmployeeRank_model Get(int _id)
            {
                return (from x in _conn.EmployeeRanks
                        where x.EmpRank_ID == _id
                        select x
                ).AsEnumerable()
                .Select(d => new EmployeeRank_model()
                {
                    Id = int.Parse(d.EmpRank_ID.ToString()),
                    Description = d.EmployeeRank1,
                    Status = d.Status,
                    UserId = d.Userid,
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.Date_created
                }).SingleOrDefault();
            }

            public int Manage(EmployeeRank_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_MASTER_EMPLOYEERANK(
                    _model.Mode,
                    _model.Description,
                    _model.Id,
                    _model.UserId,
                    _return_value
                );

                return Convert.ToInt32(_return_value.Value);
            }
        }

        public class SalaryType_repository
        {
            public apwdbEntities _conn { get; set; }

            public SalaryType_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public List<SalaryType_list_model> Get()
            {
                return (from x in _conn.Salarytypes
                        select x
                ).AsEnumerable()
                .Select(d => new SalaryType_list_model()
                {
                    Id = int.Parse(d.Salarytype_ID.ToString()),
                    Description = d.Salarytype_Desc,
                    Status = d.Status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.Date_created.ToShortDateString()
                }).ToList();
            }

            public SalaryType_model Get(int _id)
            {
                return (from x in _conn.Salarytypes
                        where x.Salarytype_ID == _id
                        select x
                ).AsEnumerable()
                .Select(d => new SalaryType_model()
                {
                    Id = int.Parse(d.Salarytype_ID.ToString()),
                    Description = d.Salarytype_Desc,
                    Status = d.Status,
                    UserId = d.UserID,
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.Date_created
                }).SingleOrDefault();
            }

            public int Manage(SalaryType_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_MASTER_SALARYTYPE(
                    _model.Mode,
                    _model.Description,
                    _model.Id,
                    _model.UserId,
                    _return_value
                );

                return Convert.ToInt32(_return_value.Value);
            }
        }

        public class Shift_repository
        {
            public apwdbEntities _conn { get; set; }

            public Shift_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public List<Shift_list_model> Get()
            {
                return (from x in _conn.Shifts
                        select x
                ).AsEnumerable()
                .Select(d => new Shift_list_model()
                {
                    Id = int.Parse(d.Shift_ID.ToString()),
                    Description = d.Description,
                    ShiftIn = d.Shift_in,
                    ShiftOut = d.Shift_out,
                    GraceMinute = d.Grace_period,
                    Status = d.status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created.ToShortDateString()
                }).ToList();
            }

            public Shift_model Get(int _id)
            {
                return (from x in _conn.Shifts
                        where x.Shift_ID == _id
                        select x
                ).AsEnumerable()
                .Select(d => new Shift_model()
                {
                    Id = int.Parse(d.Shift_ID.ToString()),
                    Description = d.Description,
                    ShiftIn = d.Shift_in,
                    ShiftOut = d.Shift_out,
                    GraceMinute = d.Grace_period,
                    BreakTimeStart = d.break_time_start,
                    BreakTimeEnd = d.break_time_end,
                    IsGraveYard = d.is_graveyard,
                    CountryId = d.country_id,
                    GraveTimeIn = d.grave_time_in,
                    GraveTimeOut = d.grave_time_out,
                    IsHalfDay = d.is_halfday,
                    IsCompress = d.is_compress,
                    Status = d.status,
                    UserId = d.UserID,
                    CreatedBy = d.SYS_USER != null ? d.SYS_USER.username : string.Empty,
                    DateCreated = d.date_created
                }).SingleOrDefault();
            }

            public int Manage(Shift_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_MASTER_SHIFT(
                    _model.Mode,
                    _model.Description,
                    _model.ShiftIn,
                    _model.ShiftOut,
                    _model.GraceMinute,
                    _model.Id,
                    _model.CountryId,   

                    _model.UserId,
                    _return_value,
                    _model.BreakTimeStart,
                    _model.BreakTimeEnd
                );

                return Convert.ToInt32(_return_value.Value);
            }
        }

        public class DocumentType_repository
        {
            public apwdbEntities _conn { get; set; }

            public DocumentType_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
                //_conn = new apwdbEntities();
            }

            public List<DocumentType_list_model> Get()
            {
                return (from x in _conn.Documents
                        select x
                ).AsEnumerable()
                .Select(d => new DocumentType_list_model()
                {
                    Id = int.Parse(d.DocId.ToString()),
                    Description = d.Description,
                    DocumentClass = d.Document_class,                    
                    Status = d.status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created.ToShortDateString()
                }).ToList();
            }

            public DocumentType_model Get(int _id)
            {
                return (from x in _conn.Documents
                        where x.DocId == _id
                        select x
                ).AsEnumerable()
                .Select(d => new DocumentType_model()
                {
                    Id = int.Parse(d.DocId.ToString()),
                    Description = d.Description,
                    DocumentClass = d.Document_class,
                    CountryId = d.country_id,
                    IsAccessibleOutside = d.accessible_outside,
                    Status = d.status,
                    UserId = d.user_id,
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created
                }).SingleOrDefault();
            }

            public int Manage(DocumentType_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.SP_H_MASTER_DOCUMENT(
                    _model.Id,
                    _model.Description,
                    _model.DocumentClass,
                    _model.CountryId,
                    _model.Mode,
                    _model.UserId,                    
                    _return_value
                );

                return Convert.ToInt32(_return_value.Value);
            }
        }

        public class Holiday_repository
        {
            public apwdbEntities _conn { get; set; }

            public Holiday_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public List<HolidayList_model> Get(int? year = null)
            {
                int targetYear = year ?? DateTime.Now.Year;

                var holidays = _conn.USP_C_GET_HOLIDAYS(targetYear).ToList();

                return holidays.Select(h => new HolidayList_model
                {
                    HolidayId = h.HolidayId ?? 0,
                    HolidayDescription = h.HolidayDescription,
                    HolidayDate = h.HolidayDate,
                    UserId = h.UserId,
                    HolidayType = h.HolidayType?.ToString() ?? "0",
                    HolidayTypeDesc = h.HolidayTypeDesc,
                    WorkingHoliday = h.WorkingHoliday ?? false,
                    DateCreated = h.DateCreated,
                    AssignedCount = _conn.TAMS_LOCAL_HOLIDAY_DET.Count(d => d.holiday_id == (h.HolidayId ?? 0))
                })
                .OrderBy(h => h.HolidayDate)
                .ToList();
            }

            public List<LocalHolidayDetailViewModel> GetLocalHolidayDetails(int holidayId)
            {
                return (from d in _conn.TAMS_LOCAL_HOLIDAY_DET
                        join e in _conn.Employees on d.emp_id equals e.Emp_ID
                        where d.holiday_id == holidayId
                        orderby d.date_created descending
                        select new LocalHolidayDetailViewModel
                        {
                            Id = d.id,
                            HolidayId = d.holiday_id,
                            EmpId = d.emp_id,
                            UserId = d.user_id,
                            DateCreated = d.date_created,
                            EmployeeName = e.Firstname + " " + e.Lastname,
                            EmpNo = e.Emp_No
                        }).ToList();
            }

            public int ManageLocalHoliday(LocalHoliday_Input_model _model)
            {
                System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                _conn.USP_T_MANAGE_LOCAL_HOLIDAY(
                    _model.Id,
                    _model.HolidayDate,
                    _model.HolidayDescription,
                    _model.HolidayType,
                    _model.Mode,
                    _model.UserId,
                    _return_value
                );

                return Convert.ToInt32(_return_value.Value);
            }

            public int ManageLocalHolidayBulkInsert(List<LocalHolidayDetail_Input_model> modelList)
            {
                if (modelList == null || !modelList.Any())
                    return 0;

                int processedCount = 0;
                
                using (var transaction = _conn.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in modelList)
                        {
                            var returnParam = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

                            _conn.USP_T_MANAGE_LOCAL_HOLIDAY_DET(
                                item.Id,
                                item.HolidayId,
                                item.EmpId,
                                item.Mode,
                                item.UserId,
                                returnParam
                            );

                            if (returnParam.Value != null && returnParam.Value != DBNull.Value && Convert.ToInt32(returnParam.Value) > 0)
                            {
                                processedCount++;
                            }
                        }

                        transaction.Commit();
                        return processedCount;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; 
                    }
                }
            }
        }

        public class Bank_repository
        {
            public apwdbEntities _conn { get; set; }

            public Bank_repository()
            {
                if (_conn == null) { _conn = new apwdbEntities(); }
            }

            public BankType_list_model Get(int _id)
            {
                return (from x in _conn.SYS_BANK
                        select x
                ).AsEnumerable()
                .Select(d => new BankType_list_model()
                {
                    Id = int.Parse(d.id.ToString()),
                    BankCode = d.Bank_code,
                    BankDescription = d.Bank_Name,
                    Status = d.status == true ? "Active" : "Inactive",
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created.ToShortDateString()
                }).SingleOrDefault();
            }
        }
    }
}