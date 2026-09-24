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

        //=========================BEGIN EMPLOYEE LEAVE BALANCE MONITORING ================================================
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
        //=========================END EMPLOYEE LEAVE BALANCE MONITORING ================================================


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
        public HttpResponseMessage GetEmployeeFiledLeaveMonitoring(
            int ClientId, string Status, bool ByMonthYear, int MonthLeave, int YearLeave, bool ByDate, DateTime DateFrom, DateTime DateTo, string keyword, int CompanyId)
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
                    keyword = keyword,
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
        //=========================END EMPLOYEE LEAVE================================================
    }
}
