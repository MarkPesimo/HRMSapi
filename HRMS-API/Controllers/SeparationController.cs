using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Floating.EmployeeTransaction_model;
using static HRModel.ViewModel.Floating.Replacement_model;
using static HRModel.ViewModel.Floating.Separation_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class SeparationController : ApiController
    {
        public GlobalRepository _globalrepository { get; set; }
        public SeparationRepository _separationrepository { get; set; }
        public ContractRepository _contractrepository { get; set; }
        public ReplacementRepository _replacementrepository { get; set; }

        public SeparationController()
        {
            if (_separationrepository == null) { _separationrepository = new SeparationRepository(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_contractrepository == null) { _contractrepository = new ContractRepository(); }
            if (_replacementrepository == null) { _replacementrepository = new ReplacementRepository(); }
        }

        [Route("api/Separation/Reasons")]
        [HttpGet]
        public HttpResponseMessage Reasons()
        {
            try
            {
                List<SeparationReason_model> results = _separationrepository.GetSeparationReasons();

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Separation/GetEmployeeSeparationRecord/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSeparationRecord(int Id)
        {
            try
            {
                EmployeeSeparationRecord_model _obj = _separationrepository.GetEmployeeSeparationRecord(Id);

                return _obj != null
                    ? Request.CreateResponse(HttpStatusCode.OK, _obj)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        [Route("api/Separation/ManageEmployeeSeparation")]
        [HttpPost]
        public HttpResponseMessage ManageEmployeeSeparation([FromBody] EmployeeSeparationRecord_model model)
        {
            try
            {
                if (model.SeparationDate < DateTime.Now)
                {
                    TransactionLog _log = new TransactionLog();
                    _log.TranId = model.Id;
                    _log.TranAction = "Inactive";
                    _log.UserId = model.UserId;
                    _log.DateValue = model.SeparationDate;

                    _globalrepository.ManageEmployeeTransactionLog(_log);
                }

                int _id = _separationrepository.ManageEmployeeSeparationRecord(model);
                if (_id != 0)
                {
                    //manage contract separation
                    if (model.TransactionId != 0)
                    {
                        Separation _separation = new Separation();

                        _separation.Id = model.TransactionId;
                        _separation.DateCreated = DateTime.Now;
                        _separation.DateInactive = model.SeparationDate;
                        _separation.SeparationId = _id;
                        _separation.InactiveById = model.UserId;

                        _id = _contractrepository.ManageEmployeeContractSepartion(_separation);

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

        [Route("api/Separation/UpdateSeparationDate")]
        [HttpPost]
        public HttpResponseMessage UpdateSeparationDate([FromBody] Separation model)
        {
            try
            {
                if (model.DateInactive < DateTime.Now.Date)
                {
                    TransactionLog _log = new TransactionLog();
                    _log.TranId = model.Id;
                    _log.TranAction = "inactive";
                    _log.UserId = model.LoginUserId;
                    _log.DateValue = model.DateInactive.Value;

                    _globalrepository.ManageEmployeeTransactionLog(_log);
                }
 
                model.Mode = 44;
                model.SeparationId = 0;

                model.Id = _contractrepository.ManageEmployeeContractSeparation(model);

                if (model.Id != 0) { return Request.CreateResponse(HttpStatusCode.OK, model.Id); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error"); }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Separation/Link")]
        [HttpPost]
        public HttpResponseMessage Link([FromBody] Separation model)
        {
            try
            {
                model.Mode = 444;
                model.Id = _contractrepository.ManageEmployeeContractSeparation(model);

                if (model.Id != 0) { return Request.CreateResponse(HttpStatusCode.OK, model.Id); }
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
