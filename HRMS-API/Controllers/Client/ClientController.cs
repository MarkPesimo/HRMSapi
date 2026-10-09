using HRMS_API.Repository;
using HRMS_API.Repository.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static HRModel.ViewModel.Client.Client_model;
using static HRModel.ViewModel.Client.ClientHistory_model;
using static HRModel.ViewModel.Client.ClientSetting_model;
using static HRModel.ViewModel.Client.ClientSetting_model.AccountMapping;
using static HRModel.ViewModel.Client.ClientSetting_model.PayrollSetup;

namespace HRMS_API.Controllers.Client
{
    public class ClientController : ApiController
    {
        private GlobalRepository _globalrepository { get; set; }
        private CompanyRepository _companyrepository { get; set; }
        private ClientProfileRepository _profilerepository { get; set; }
        private ClientDocumentRepository _documentrepository { get; set; }
        private ClientDepartmentRepository _departmentrepository { get; set; }
        private ClientBranchRepository _branchrepository { get; set; }
        private ClientBankRepository _bankrepository { get; set; }
        private ClientContactRepository _contactrepository { get; set; }
        private ClientAdjustmentRepository _adjustmentrepository { get; set; }
        private ClientDeductionRepository _deductionrepository { get; set; }
        private ClientShiftRepository _shiftrepository { get; set; }
        private ClientHistoryRepository _historyrepository { get; set; }
        private ClientSetupRepository _setuprepository { get; set; }


        public ClientController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_companyrepository == null) { _companyrepository = new CompanyRepository(); }
            if (_profilerepository == null) { _profilerepository = new ClientProfileRepository(); }
            if (_documentrepository == null) { _documentrepository = new ClientDocumentRepository(); }
            if (_departmentrepository == null) { _departmentrepository = new ClientDepartmentRepository(); }
            if (_bankrepository == null) { _bankrepository = new ClientBankRepository(); }
            if (_contactrepository == null) { _contactrepository = new ClientContactRepository(); }
            if (_adjustmentrepository == null) { _adjustmentrepository = new ClientAdjustmentRepository(); }
            if (_deductionrepository == null) { _deductionrepository = new ClientDeductionRepository(); }
            if (_shiftrepository == null) { _shiftrepository = new ClientShiftRepository(); }
            if (_historyrepository == null) { _historyrepository = new ClientHistoryRepository(); }
            if (_setuprepository == null) { _setuprepository = new ClientSetupRepository(); }
            if (_branchrepository == null) { _branchrepository = new ClientBranchRepository(); }
        }

        //=========================================SHIFT==========================================
        [Route("api/Client/Shifts/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Shifts(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientShift_vw_model> _model = _shiftrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetShiftDropdownList")]
        [HttpGet]
        public HttpResponseMessage GetShiftDropdownList()
        {
            try
            {
                var _list = _shiftrepository.GetShiftDropdownList();
                if (_list != null && _list.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _list);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new List<ShiftDropdown_model>());
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Shift/{Id}")]
        [HttpGet]
        public HttpResponseMessage Shift(int Id)
        {
            try
            {
                ClientShift_model _model = _shiftrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageShift")]
        [HttpPost]
        public HttpResponseMessage ManageShift([FromBody] ClientShift_model model)
        {
            try
            {
                int result = _shiftrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================SHIFT==========================================


        //=========================================DEDUCTION==========================================
        [Route("api/Client/Deductions/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Deductions(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientDeduction_vw_model> _model = _deductionrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Deduction/{Id}")]
        [HttpGet]
        public HttpResponseMessage Deduction(int Id)
        {
            try
            {
                ClientDeduction_model _model = _deductionrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageDeduction")]
        [HttpPost]
        public HttpResponseMessage ManageContact([FromBody] ClientDeduction_model model)
        {
            try
            {
                int result = _deductionrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================DEDUCTION==========================================

        //=========================================ADJUSTMENT==========================================
        [Route("api/Client/Adjustments/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Adjustments(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientAdjustment_vw_model> _model = _adjustmentrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetClassificationList")]
        [HttpGet]
        public HttpResponseMessage GetClassificationList()
        {
            try
            {
                var _list = _adjustmentrepository.GetClassificationList();
                if (_list != null && _list.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _list);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new List<ClientAdjustment_vw_model>());
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Adjustment/{Id}")]
        [HttpGet]
        public HttpResponseMessage Adjustment(int Id)
        {
            try
            {
                ClientAdjustment_model _model = _adjustmentrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageAdjustment")]
        [HttpPost]
        public HttpResponseMessage ManageContact([FromBody] ClientAdjustment_model model)
        {
            try
            {
                int result = _adjustmentrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================ADJUSTMENT==========================================


        //=========================================CONTACT==========================================
        [Route("api/Client/Contacts/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Contacts(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientContact_vw_model> _model = _contactrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Contact/{Id}")]
        [HttpGet]
        public HttpResponseMessage Contact(int Id)
        {
            try
            {
                ClientContact_model _model = _contactrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageContact")]
        [HttpPost]
        public HttpResponseMessage ManageContact([FromBody] ClientContact_model model)
        {
            try
            {
                int result = _contactrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================CONTACT==========================================

        //=========================================BANK==========================================
        [Route("api/Client/GetDropdownBanks")]
        [HttpGet]
        public HttpResponseMessage GetDropdownBanks()
        {
            try
            {
                List<SysBank_vw_model> _model = _bankrepository.GetList();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Banks/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Banks(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientBank_vw_model> _model = _bankrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Bank/{Id}")]
        [HttpGet]
        public HttpResponseMessage Bank(int Id)
        {
            try
            {
                ClientBank_model _model = _bankrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageBank")]
        [HttpPost]
        public HttpResponseMessage ManageBranch([FromBody] ClientBank_model model)
        {
            try
            {
                int result = _bankrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================BANK==========================================

        //=========================================BRANCH==========================================
        [Route("api/Client/GetRegionDropdown")]
        [HttpGet]
        public HttpResponseMessage GetRegionDropdown()
        {
            try
            {
                List<SysRegion_vw_model> _model = _branchrepository.GetRegionDropdown();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetBranches/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetBranches(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientBranch_vw_model> _model = _branchrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Branch/{Id}")]
        [HttpGet]
        public HttpResponseMessage Branch(int Id)
        {
            try
            {
                ClientBranch_model _model = _branchrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageBranch")]
        [HttpPost]
        public HttpResponseMessage ManageBranch([FromBody] ClientBranch_model model)
        {
            try
            {
                int result = _branchrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================BRANCH==========================================

        //=========================================PROFILE==========================================
        [Route("api/Client/Get/{AppId}/{Keyword}/{CompanyGUID}")]
        [HttpGet]
        public HttpResponseMessage GetProfile(int AppId, string Keyword, string CompanyGUID)
        {
            try
            {
                int _companyid = _companyrepository.GetCompanyId(CompanyGUID);
                List<ClientMonitoring_model> _model = _profilerepository.GetMonitoring(AppId, Keyword, _companyid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetProfile/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetProfile(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                ClientProfile_model _model = _profilerepository.Get(_clientid);
                if (_model != null ) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageProfile")]
        [HttpPost]
        public HttpResponseMessage ManageProfile([FromBody] ClientProfile_model model)
        {
            try
            {
                int result = _profilerepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================PROFILE==========================================

        //=========================================DOCUMENT==========================================
        [Route("api/Client/GetDocumentDropdown")]
        [HttpGet]
        public HttpResponseMessage GetDocumentDropdown()
        {
            try
            {
                List<Document_vw_model> _model = _documentrepository.GetDocumentDropdown();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Documents/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Documents(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientDocument_vw_model> _model = _documentrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Document/{Id}")]
        [HttpGet]
        public HttpResponseMessage Document(int Id)
        {
            try
            {
                ClientDocument_model _model = _documentrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageDocument")]
        [HttpPost]
        public HttpResponseMessage ManageDocument([FromBody] ClientDocument_model model)
        {
            try
            {
                int result = _documentrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================DOCUMENT==========================================

        //=========================================DEPARTMENT==========================================
        [Route("api/Client/GetDepartmentDropdown")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentDropdown()
        {
            try
            {
                List<Department_vw_model> _model = _departmentrepository.GetDepartmentList();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Departments/{GUID}")]
        [HttpGet]
        public HttpResponseMessage Departments(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientDepartment_vw_model> _model = _departmentrepository.GetList(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/Department/{Id}")]
        [HttpGet]
        public HttpResponseMessage Department(int Id)
        {
            try
            {
                ClientDepartment_model _model = _departmentrepository.Get(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageDepartment")]
        [HttpPost]
        public HttpResponseMessage ManageDepartment([FromBody] ClientDepartment_model model)
        {
            try
            {
                int result = _departmentrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================DEPARTMENT==========================================

        //=========================================TRANSACTION HISTORY==========================================
        [Route("api/Client/JobOrderHistory/{GUID}")]
        [HttpGet]
        public HttpResponseMessage JobOrderHistory(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientJobOrder_vw_model> _model = _historyrepository.GetJobOrders(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/PayrollRegisterHistory/{GUID}/{Month}/{Year}")]
        [HttpGet]
        public HttpResponseMessage PayrollRegisterHistory(string GUID, int Month, int Year)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientPayrollRegister_vw_model> _model = _historyrepository.GetPayrollRegisters(_clientid, Month, Year);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ServiceInvoiceHistory/{GUID}/{Month}/{Year}")]
        [HttpGet]
        public HttpResponseMessage ServiceInvoiceHistory(string GUID, int Month, int Year)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientServiceInvoice_vw_model> _model = _historyrepository.GetInvoices(_clientid, Month, Year);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/CreditMemoHistory/{GUID}/{Month}/{Year}")]
        [HttpGet]
        public HttpResponseMessage CreditMemoHistory(string GUID, int Month, int Year)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientCreditMemo_vw_model> _model = _historyrepository.GetCreditMemos(_clientid, Month, Year);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/OfficialReceiptHistory/{GUID}/{Month}/{Year}")]
        [HttpGet]
        public HttpResponseMessage OfficialReceiptHistory(string GUID, int Month, int Year)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<ClientOfficialReceipt_vw_model> _model = _historyrepository.GetOfficialReceipts(_clientid, Month, Year);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================TRANSACTION HISTORY==========================================

        //=========================================CLIENT PAYROLL SETUP==========================================
        [Route("api/Client/GetOvertimeBasisList")]
        [HttpGet]
        public HttpResponseMessage GetOvertimeBasisList()
        {
            try
            {
                List<OvertimeBasis_vw_model> _model = _setuprepository.GetOvertimeBasisList();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetPayrollSetup/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetPayrollSetup(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                PayrollSetup _model = _setuprepository.GetPayrollSetup(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManagePayrollSetup")]
        [HttpPost]
        public HttpResponseMessage ManagePayrollSetup([FromBody] PayrollSetup model)
        {
            try
            {
                int result = _setuprepository.ManagePayrollSetup(model);

                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, model.ClientId); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================CLIENT SETUP==========================================

        //=========================================CLIENT PAYROLL SETUP==========================================
        [Route("api/Client/GetInterimSetup/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetInterimSetup(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                InterimSetup _model = _setuprepository.GetInterimSetup(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageInterimSetup")]
        [HttpPost]
        public HttpResponseMessage ManageInterimSetup([FromBody] InterimSetup model)
        {
            try
            {
                int result = _setuprepository.ManageInterimSetup(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, model.ClientId); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================CLIENT SETUP==========================================

        //=========================================CLIENT PAYROLL SETUP==========================================
        [Route("api/Client/GetPayrollServiceSetup/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetPayrollServiceSetup(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                PayrollService _model = _setuprepository.GetPayrollServiceSetup(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManagePayrollServiceSetup")]
        [HttpPost]
        public HttpResponseMessage ManagePayrollServiceSetup([FromBody] PayrollService model)
        {
            try
            {
                int result = _setuprepository.ManagePayrollServiceSetup(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, model.ClientId); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================CLIENT SETUP==========================================

        //=========================================CLIENT OVERTIME RATE SETUP==========================================
        [Route("api/Client/GetSysEntries")]
        [HttpGet]
        public HttpResponseMessage GetSysEntries()
        {
            try
            {
                List<SysEntry> _model = _setuprepository.GetSysEntries();

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

        [Route("api/Client/GetOvertimeRate/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetOvertimeRate(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                OvertimeRate _model = _setuprepository.GetOvertimeRate(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageOvertimeRate")]
        [HttpPost]
        public HttpResponseMessage ManageOvertimeRate([FromBody] OvertimeRate model)
        {
            try
            {
                int result = _setuprepository.ManageOvertimeRate(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, model.ClientId); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================CLIENT OVERTIME RATE SETUP==========================================

        //=========================================CLIENT ACCOUNT SETUP==========================================
        [Route("api/Client/GetAccounts/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetAccounts(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                List<Account_model> _model = _setuprepository.GetAccounts(_clientid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetAccount/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetAccount(int Id)
        {
            try
            {
                Account_model _model = _setuprepository.GetAccount(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageAccount")]
        [HttpPost]
        public HttpResponseMessage ManageAccount([FromBody] Account_model model)
        {
            try
            {
                int result = _setuprepository.ManageAccount(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, model.ClientId); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }


        [Route("api/Client/GetAccountDetail/{Id}")]
        [HttpPost]
        public HttpResponseMessage GetAccountDetail(int Id)
        {
            try
            {
                AccountDetail_model _model = _setuprepository.GetAccountDetail(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/GetAccountDetails/{AccountId}")]
        [HttpPost]
        public HttpResponseMessage GetAccountDetails(int AccountId)
        {
            try
            {
                List<AccountDetail_model> _model = _setuprepository.GetAccountDetails(AccountId);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Client/ManageAccountDetail")]
        [HttpPost]
        public HttpResponseMessage ManageAccountDetail([FromBody] AccountDetail_model model)
        {
            try
            {
                int result = _setuprepository.ManageAccountDetail(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, model.AccountId); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=========================================CLIENT ACCOUNT SETUP==========================================

        //==================================CLIENT PORTAL RULE SETUP===========================================
        [Route("api/Client/GetPortalRule/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetPortalRule(string GUID)
        {
            try
            {
                int _clientid = _profilerepository.GetClientId(GUID);
                ClientPortalRule_model _model = _setuprepository.GetPortalRule(_clientid);

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

        [Route("api/Client/ManagePortalFilingRule")]
        [HttpPost]
        public HttpResponseMessage ManagePortalFilingRule([FromBody] ClientPortalRule_model model)
        {
            try
            {
                int result = _setuprepository.ManagePortalFilingRule(model);
                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, model.Id);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record.");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //==================================CLIENT PORTAL RULE SETUP===========================================
    }
}
