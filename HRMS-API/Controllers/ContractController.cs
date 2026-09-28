using HRModel.ViewModel.Contract.EmployeeTransaction;
using HRModel.ViewModel.Employees;
using HRModel.ViewModel.Global;
using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Contract.EmployeeTransaction.EmployeeTransaction;
using static HRModel.ViewModel.Global.GlobalSearch_models;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class ContractController : ApiController
    {
        private ContractRepository Contractrepository { get; set; }
        private GlobalRepository GlobalRepository { get; set; }
        private EmployeeRepository EmployeeRepository { get; set; }

        public ContractController()
        {
            if (Contractrepository == null) { Contractrepository = new ContractRepository(); }
            if (GlobalRepository == null) { GlobalRepository = new GlobalRepository(); }
            if (EmployeeRepository == null) { EmployeeRepository = new EmployeeRepository(); }
        }

        [Route("api/Contract/GetMonitoring/{_clientid}/{_keyword}/{_bydate}/{_stage}/{_from}/{_to}/{_bycontractstatus}/{_contractstatus}/{_byemployeetype}/{_employeetypeid}/{_byuserhired}/{_userid}/{_company_id}")]
        [HttpGet]
        public HttpResponseMessage GetMonitoring(int _clientid, string _keyword, bool _bydate, string _stage, DateTime _from, DateTime _to, bool _bycontractstatus, bool _contractstatus, bool _byemployeetype, int _employeetypeid, bool _byuserhired, int _userid, int _company_id)
        {
            try
            {
                List<EmployeeTransaction.Monitoring> _model = Contractrepository.GetMonitoring(_clientid, _keyword, _bydate, _stage, _from,
                    _to, _bycontractstatus, _contractstatus, _byemployeetype, _employeetypeid,
                    _byuserhired, _userid, _company_id);

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetClientList/{_company_id}/{_keyword}")]
        [HttpGet]
        public HttpResponseMessage GetClientList(int _company_id, string _keyword = "")
        {
            try
            {
                List<EmployeeTransaction.ClientList> _model = GlobalRepository.GetClientList(_company_id, _keyword);

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetEmployeeList/{_clientid}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeList(int _clientid)
        {
            try
            {
                List<EmployeeSearchList> _model = GlobalRepository.SearchEmployess(_clientid);

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetEmployeeTypeList")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeTypeList()
        {
            try
            {
                List<EmployeeTypeModel> _model = GlobalRepository.GetEmployeeTypes();

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetEmployeeRankList")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeRankList()
        {
            try
            {
                List<EmployeeRankModel> _model = GlobalRepository.GetEmployeeRanks();

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetClientDepartments/{_clientid}")]
        [HttpGet]
        public HttpResponseMessage GetClientDepartments(int _clientid)
        {
            try
            {
                List<DepartmentModel> _model = GlobalRepository.GetClientDepartments(_clientid);

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetClientBranches/{_clientid}")]
        [HttpGet]
        public HttpResponseMessage GetClientBranches(int _clientid)
        {
            try
            {
                List<BranchModel> _model = GlobalRepository.GetClientBranches(_clientid);

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetClientShifts/{_clientid}")]
        [HttpGet]
        public HttpResponseMessage GetClientShifts(int _clientid)
        {
            try
            {
                List<ShiftModel> _model = GlobalRepository.GetClientShifts(_clientid);

                if (_model != null && _model.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/ManageContract")]
        [HttpPost]
        public HttpResponseMessage ManageContract([FromBody] Contract model)
        {
            try
            {
                int result = Contractrepository.ManageContract(model);

                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Contract transaction saved successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction.");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }


        //==========================CONTRACT EXTENSION==============================
        [Route("api/Contract/Extension")]
        [HttpPost]
        public HttpResponseMessage Extension([FromBody] Contract model)
        {
            try
            {
                model.HireType = "Extension";
                model.mode = 5;

                int result = Contractrepository.ManageContract(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Contract transaction successfully extend."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //==========================CONTRACT EXTENSION==============================

        //==========================CONTRACT REHIRE==============================
        [Route("api/Contract/Rehire")]
        [HttpPost]
        public HttpResponseMessage Rehire([FromBody] EmployeeRehire_model model)
        {
            try
            {
                int result = Contractrepository.ManageRehire(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Contract transaction successfully Rehire."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //==========================CONTRACT REHIRE==============================


        //==========================CONTRACT REMARKS==============================
        [Route("api/Contract/Remarks/{Id}/{Var}")]
        [HttpGet]
        public HttpResponseMessage Remarks(int Id, int Var)
        {
            try
            {
                ContractRemarks _model = Contractrepository.GetContractRemark(Id);

                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        
        [Route("api/Contract/Remarks/{ContractId}")]
        [HttpGet]
        public HttpResponseMessage Remarks(int ContractId)
        {
            try
            {
                List<ContractRemarks> _model = Contractrepository.GetContractRemarks(ContractId);

                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/ManageContractRemarks")]
        [HttpPost]
        public HttpResponseMessage ManageContractRemarks([FromBody] ContractRemarks model)
        {
            try
            {
                int result = Contractrepository.ManageContractRemarks(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Contract remarks successfully saved."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save contract transaction."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Contract/GetContract/{_id}")]
        [HttpGet]
        public HttpResponseMessage GetContract(int _id)
        {
            try
            {
                Contract _model = Contractrepository.GetContract(_id);

                if (_model != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
    }
}