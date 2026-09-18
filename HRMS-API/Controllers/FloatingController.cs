using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Floating.EmployeeTransaction_model;
using static HRModel.ViewModel.Floating.Floating_model;
using static HRModel.ViewModel.Floating.Replacement_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class FloatingController : ApiController
    {
        public GlobalRepository _globalrepository { get; set; }
        public FloatingRepository _floatingrepository { get; set; }
        public TransactionRepository _transactionrepository { get; set; }
        public ReplacementRepository _replacementrepository { get; set; }

        public FloatingController()
        {
            if (_floatingrepository == null) { _floatingrepository = new FloatingRepository(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_transactionrepository == null) { _transactionrepository = new TransactionRepository(); }
            if (_replacementrepository == null) { _replacementrepository = new ReplacementRepository(); }

        }

        [Route("api/Floating/Reasons")]
        [HttpGet]
        public HttpResponseMessage Reasons( )
        {
            try
            {
                List<FloatingReason_model> results = _floatingrepository.GetFloatingReasons();

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Floating/GetEmployeeFloatingRecord/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeFloatingRecord(int Id)
        {
            try
            {
                EmployeeFloatingRecord_model _obj = _floatingrepository.GetEmployeeFloatingRecord(Id);

                return _obj != null  
                    ? Request.CreateResponse(HttpStatusCode.OK, _obj)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("api/Floating/ManageEmployeeFloating")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeFloating([FromBody] EmployeeFloatingRecord_model model)
        {
            try
            {
                if (model.FloatingDate < DateTime.Now)
                {
                    TransactionLog _log = new TransactionLog();
                    _log.TranId = model.Id;
                    _log.TranAction = "Floating";
                    _log.UserId = model.UserId;
                    _log.DateValue = model.FloatingDate;

                    _globalrepository.ManageEmployeeTransactionLog(_log);
                }

                int _id = _floatingrepository.ManageEmployeeFloatingRecord(model);
                if (_id != 0)
                {
                    //manage contract floating
                    if (model.TransactionId != 0)
                    {
                        Separation _floating = new Separation();

                        _floating.Id = model.TransactionId;
                        _floating.DateResignationSubmitted = DateTime.Now;
                        _floating.DateSeparated = model.FloatingDate;
                        _floating.FloatingId = _id;
                        _floating.SeparatedById = model.UserId;
                        _floating.LoginUserId = model.UserId;
                        _floating.Mode = 3;

                        _id = _transactionrepository.ManageEmployeeContractFloating(_floating);

                        if (_id > 0)
                        {
                            if (model.ReplacementDate != null)
                            {
                                EmployeeReplacement_model _replacement = new EmployeeReplacement_model();
                                _replacement.ReplacementDate = model.ReplacementDate;
                                _replacement.EmpId = model.EmpId;
                                _replacement.Id = model.ReplacementId;
                                _replacement.Remarks = model.ReplacementRemarks;
                                _replacement.FloatingId = model.Id;

                                _replacement.Id = _replacementrepository.ManageReplacement(_replacement);

                            }
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, _id);
                }
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
