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
                    Status = Status,
                    Keyword = Keyword,
                    CompanyId = CompanyId
                };

                List<EmployeeLoanMonitoringModel> results = _loanrepository.GetEmployeeLoanMonitoring(filter);

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
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
    }
}
