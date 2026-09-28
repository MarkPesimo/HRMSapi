using HRModel.ViewModel.Employees;
using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Loan.Loan_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]
    public class LoanController : ApiController
    {
        public LoanRepository _loanrepository { get; set; }
        public GlobalRepository _globalrepository{ get; set; }

        public LoanController()
        {
            if (_loanrepository == null) { _loanrepository = new LoanRepository(); }
        }

        [Route("api/Loan/GetEmployeeLoanMonitoring/{ByLoan}/{LoanTypeId}/{ByDate}/{From}/{To}/{ByStatus}/{Status}/{Keyword}/{CompanyId}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeLoanMonitoring(
            bool ByLoan, int LoanTypeId, bool ByDate, DateTime From, DateTime To, bool ByStatus, string Status, string Keyword, int CompanyId)
        {
            try
            {
                var filter = new LoanFilter_model
                {
                    ByLoan = ByLoan,
                    LoanTypeId = LoanTypeId,
                    ByDate = ByDate,
                    From = From,
                    To = To,
                    ByStatus = ByStatus,
                    Status = (string.IsNullOrWhiteSpace(Status) || Status.Equals("NULL", StringComparison.OrdinalIgnoreCase)) ? "" : Status,
                    Keyword = (string.IsNullOrWhiteSpace(Keyword) || Keyword.Equals("NULL", StringComparison.OrdinalIgnoreCase)) ? "" : Keyword,
                    CompanyId = CompanyId
                };

                List<EmployeeLoanMonitoringModel> results = _loanrepository.GetEmployeeLoanMonitoring(filter);
                
                return Request.CreateResponse(HttpStatusCode.OK, results ?? new List<EmployeeLoanMonitoringModel>());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("api/Loan/ManageEmployeeLoan")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeLoan([FromBody] EmployeeLoanModel model)
        {
            try
            {
                int _id = _loanrepository.ManageEmployeeLoan(model);
                if (_id != 0) { return Request.CreateResponse(HttpStatusCode.OK, _id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Loan/GetLoanTypes")]
        [HttpGet]
        public HttpResponseMessage GetLoanTypes()
        {
            try
            {
                List<LoanTypeModel> _obj = _loanrepository.GetLoanTypes();
                if (_obj != null) { return Request.CreateResponse(HttpStatusCode.OK, _obj); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }
    }
}
