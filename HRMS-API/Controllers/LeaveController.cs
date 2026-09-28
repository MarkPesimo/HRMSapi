using HRModel.ViewModel.Leave;
using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Leave.Leave_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class LeaveController : ApiController
    {
        public LeaveRepository _leaverepository { get; set; }

        public LeaveController()
        {
            if (_leaverepository == null) { _leaverepository = new LeaveRepository(); }
        }
        
        [Route("api/Leave/GetEmployeeLeaveBalanceMonitoring/{YearEntitled}/{EmpId}/{LeaveTypeId}/{ClientId}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeLeaveBalanceMonitoring(int YearEntitled, int EmpId, int LeaveTypeId, int ClientId)
        {
            try
            {
                var filter = new LeaveBalanceFilter_model
                {
                    YearEntitled = YearEntitled,
                    EmpId = EmpId,
                    LeaveTypeId = LeaveTypeId,
                    ClientId = ClientId
                };

                List<EmployeeLeaveBalanceMonitoring_model> results = _leaverepository.GetEmployeeLeaveBalanceMonitoring(filter);

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Leave/GetEmployeeLeaveBalance/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeLeaveBalance(int Id)
        {
            try
            {
                LeaveBalanceModel result = _leaverepository.GetEmployeeLeaveBalance(Id);

                return result != null 
                    ? Request.CreateResponse(HttpStatusCode.OK, result)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Leave/ManageEmployeeLeaveBalance")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeLeaveBalance([FromBody] LeaveBalanceModel model)
        {
            try
            {
                int _id = _leaverepository.ManageEmployeeLeaveBalance(model);
                if (_id != 0) { return Request.CreateResponse(HttpStatusCode.OK, _id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

        }


        //=========================BEGIN EMPLOYEE LEAVE================================================
        [Route("api/Leave/GetFiledLeaveForApproval/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetFiledLeaveForApproval(int Id)
        {
            try
            {
                LeaveForApproval_model result = _leaverepository.GetFiledLeaveForApproval(Id);

                return result != null
                    ? Request.CreateResponse(HttpStatusCode.OK, result)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Leave/GetEmployeeFiledLeaveMonitoring/{ClientId}/{Status}/{ByMonthYear}/{MonthLeave}/{YearLeave}/{ByDate}/{DateFrom}/{DateTo}/{keyword}/{CompanyId}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeFiledLeaveMonitoring(int ClientId, string Status, bool ByMonthYear, int MonthLeave, int YearLeave, bool ByDate, DateTime DateFrom, DateTime DateTo, string keyword, int CompanyId)
        {
            try
            {
                var filter = new LeaveFilter_model
                {
                    ClientId = ClientId,
                    Status = Status,
                    ByMonthYear = ByMonthYear,
                    MonthLeave = MonthLeave,
                    YearLeave = YearLeave,
                    ByDate = ByDate,
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    keyword = keyword == "NULL" ? null : keyword,
                    CompanyId = CompanyId
                };

                List<EmployeeFiledLeave> results = _leaverepository.GetEmployeeFiledLeaveMonitoring(filter);

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Leave/ManageEmployeeFiledLeave")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeFiledLeave([FromBody] LeaveModel model)
        {
            try
            {
                int _id = _leaverepository.ManageEmployeeFiledLeave(model);
                if (_id != 0) { return Request.CreateResponse(HttpStatusCode.OK, _id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

        }

        [Route("api/Leave/GetActiveLeaveTypes")]
        [HttpGet]
        public HttpResponseMessage GetActiveLeaveTypes()
        {
            try
            {
                List<LeaveTypeModel> _obj = _leaverepository.GetActiveLeaveTypes();
                if (_obj != null && _obj.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _obj);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMsg);
            }
        }

        [Route("api/Leave/CoorGetLeaveBalanceMonitoring")]
        [HttpPost]
        public HttpResponseMessage CoorGetLeaveBalanceMonitoring([FromBody] LeaveBalanceFilter_model filter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new LeaveBalanceFilter_model();
                }

                var _list = _leaverepository.GetEmployeeLeaveBalanceMonitoring(filter);

                if (_list != null && _list.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _list);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No leave balance monitoring records found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Leave/GetEmployeeLeaveDetail/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeLeaveDetail(int Id)
        {
            try
            {
                EmployeeLeaveDetailMonitoring_model result = _leaverepository.GetEmployeeLeaveDetail(Id);

                return result != null
                    ? Request.CreateResponse(HttpStatusCode.OK, result)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Leave/ManageLeaveAccept")]
        [HttpPost]
        public HttpResponseMessage ManageLeaveAccept([FromBody] LeaveAcceptModel model)
        {
            try
            {
                int _id = _leaverepository.ManageLeaveAccept(model);
                if (_id != 0) { return Request.CreateResponse(HttpStatusCode.OK, _id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Leave/GetEmployeeFiledLeave/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeFiledLeave(int Id)
        {
            try
            {
                LeaveModel result = _leaverepository.GetEmployeeFiledLeave(Id);

                return result != null
                    ? Request.CreateResponse(HttpStatusCode.OK, result)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Leave/ManageLeaveRevoke")]
        [HttpPost]
        public HttpResponseMessage ManageLeaveRevoke([FromBody] LeaveAcceptModel model)
        {
            try
            {
                int _id = _leaverepository.ManageLeaveRevoke(model);
                if (_id != 0) { return Request.CreateResponse(HttpStatusCode.OK, _id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}
