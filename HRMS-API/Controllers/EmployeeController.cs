using HRModel.ViewModel.Employees;
using HRModel.ViewModel.Global;
using HRMS_API.Helper;
using HRMS_API.Repository;
using HRMS_API.Repository.Client;
using HRMS_API.Repository.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class EmployeeController : ApiController
    {
        private EmployeeRepository Employeerepository { get; set; }
        private GlobalRepository _globalrepository { get; set; }
        private EmployeeEducationRepository _employeeeducationrepository { get; set; }
        private EmployeeDocumentRepository _employeedocumentrepository { get; set; }
        private EmployeeSkillRepository _skillrepository { get; set; }
        private EmployeeEmploymentRepository _externalrepository { get; set; }
        private CompanyRepository _companyrepository { get; set; }

        public EmployeeController()
        {
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
            if (_companyrepository == null) { _companyrepository = new CompanyRepository(); }
            if (Employeerepository == null) { Employeerepository = new EmployeeRepository(); }
            if (_employeeeducationrepository == null) { _employeeeducationrepository = new EmployeeEducationRepository(); }
            if (_employeedocumentrepository == null) { _employeedocumentrepository = new EmployeeDocumentRepository(); }
            if (_skillrepository == null) { _skillrepository = new EmployeeSkillRepository(); }
            if (_externalrepository == null) { _externalrepository = new EmployeeEmploymentRepository(); }
        }

        //=================================PROFILE==========================================
        [Route("api/Employee/GetMonitoring/{Keyword}/{ByClient}/{ClientID}/{PageNo}/{PageSize}/{CompanyGUID}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeMonitoring(string Keyword, bool ByClient, int ClientID, int PageNo, int PageSize, string CompanyGUID)
        {
            try
            {
                int _companyid = _companyrepository.GetCompanyId(CompanyGUID);
                List<EmployeeMonitoringViewModel> _model =  Employeerepository.GetEmployeeMonitoring(Keyword, ByClient, ClientID, PageNo, PageSize, _companyid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetEmployeeProfile/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeProfile(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                EmployeeProfile _model = Employeerepository.GetEmployeeProfile(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model);}
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetPersonalInfo/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetPersonalInfo(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                PersonalInfo _model = Employeerepository.GetPersonalInfo(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetCities")]
        [HttpGet]
        public HttpResponseMessage GetCities()
        {
            try
            {
                List<AreaLibraryModel> _list = Employeerepository.GetCities();

                if (_list != null && _list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _list);
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

        [Route("api/Employee/GetProvinces")]
        [HttpGet]
        public HttpResponseMessage GetProvinces()
        {
            try
            {
                List<AreaLibraryModel> _list = Employeerepository.GetProvinces();

                if (_list != null && _list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _list);
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

        [Route("api/Employee/ManagePersonal")]
        [HttpPost]
        public HttpResponseMessage ManagePersonal([FromBody] PersonalInfo model)
        {
            try
            {
                int result = Employeerepository.ManagePersonal(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================PROFILE==========================================

        //=================================SPOUSE==========================================
        [Route("api/Employee/GetSpouse/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetSpouse(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                SpouseInfo _model = Employeerepository.GetSpouseInfo(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/ManageSpouse")]
        [HttpPost]
        public HttpResponseMessage ManageSpouse([FromBody] SpouseInfo model)
        {
            try
            {
                int result = Employeerepository.ManageSpouse(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================SPOUSE==========================================

        //=================================EMERGENCY CONTACT==========================================
        [Route("api/Employee/GetEmergency/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetEmergency(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                EmergencyContactInfo _model = Employeerepository.GetEmergencyContact(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/ManageEmergencyContact")]
        [HttpPost]
        public HttpResponseMessage ManageEmergencyContact([FromBody] EmergencyContactInfo model)
        {
            try
            {
                int result = Employeerepository.ManageEmergencyContact(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================EMERGENCY CONTACT==========================================


        //=================================GOVERNMENT INFO==========================================
        [Route("api/Employee/GetGovernmentNos/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetGovernmentNos(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                GovernmentNos _model = Employeerepository.GetGovernmentInfo(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/ManageGovernmentInfo")]
        [HttpPost]
        public HttpResponseMessage ManageGovernmentInfo([FromBody] GovernmentNos model)
        {
            try
            {
                int result = Employeerepository.ManageGovernmentInfo(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================GOVERNMENT INFO==========================================


        //=================================EDUCATION==========================================
        [Route("api/Employee/GetEducation/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetEducation(int Id)
        {
            try
            {
                EducationalBackgroundModel _model = _employeeeducationrepository.GetEducation(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetEducations/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetEducations(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                List<EducationalBackgroundViewModel> _model  = _employeeeducationrepository.GetEducations(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetSchools")]
        [HttpGet]
        public HttpResponseMessage GetSchools()
        {
            try
            {
                List<SchoolViewModel> _model = _employeeeducationrepository.GetSchools();
                if (_model != null && _model.Any())
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
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }

        [Route("api/Employee/GetAllDegrees")]
        [HttpGet]
        public HttpResponseMessage GetAllDegrees()
        {
            try
            {
                List<DegreeViewModel> _model = _employeeeducationrepository.GetAllDegrees();
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }

        [Route("api/Employee/GetSchoolLevels")]
        [HttpGet]
        public HttpResponseMessage GetSchoolLevels()
        {
            try
            {
                List<SchoolLevelViewModel> _model = _employeeeducationrepository.GetSchoolLevels();
                if (_model != null && _model.Any())
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
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }

        [Route("api/Employee/ManageEducation")]
        [HttpPost]
        public HttpResponseMessage ManageEducation([FromBody] EducationalBackgroundModel model)
        {
            try
            {
                bool result = _employeeeducationrepository.ManageEducation(model);
                if (result) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================EDUCATION==========================================

        //=================================INTERNAL EMPLOYMENT==========================================
        [Route("api/Employee/GetInternalEmployments/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetInternalEmployments(string GUID)
        {
            try
            {
                int _canid = _globalrepository.GetEmployeeKey(GUID).CandidateId;
                List<InternalEmploymentViewModel> _model = _externalrepository.GetInternalEmployments(_canid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }
        //=================================INTERNAL EMPLOYMENT==========================================

        //=================================EXTERNAL EMPLOYMENT==========================================
        [Route("api/Employee/GetExternalEmployments/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetExternalEmployments(string GUID)
        {
            try
            {
                int _canid = _globalrepository.GetEmployeeKey(GUID).CandidateId;
                List<PreviousEmploymentViewModel> _model = _externalrepository.GetExternalEmployments(_canid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetExternalEmployment/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetExternalEmployment(int Id)
        {
            try
            {
                PreviousEmploymentModel _model = _externalrepository.GetExternalEmployment(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/ManageExternalEmployment")]
        [HttpPost]
        public HttpResponseMessage ManageExternalEmployment([FromBody] PreviousEmploymentModel model)
        {
            try
            {
                bool result = _externalrepository.ManageExternal(model);
                 return Request.CreateResponse(HttpStatusCode.OK, "Ok.");}
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================EXTERNAL EMPLOYMENT==========================================

        //=================================SKILLS==========================================
        [Route("api/Employee/GetSkill/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetSkill(int Id)
        {
            try
            {                
                SkillModel _model = _skillrepository.GetSkill(Id);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetSkills/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetSkills(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                List<SkillViewModel> _model = _skillrepository.GetSkills(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/ManageSkill")]
        [HttpPost]
        public HttpResponseMessage ManageSkill([FromBody] SkillModel model)
        {
            try
            {
                int result = _skillrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================SKILLS==========================================

        //=================================DOCUMENT==========================================
        [Route("api/Employee/GetDocument/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetDocument(int Id)
        {
            try
            {
                EmployeeDocumentModel _model = _employeedocumentrepository.GetDocument(Id);
                if (_model != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _model);
                }
                else
                { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/GetDocuments/{GUID}")]
        [HttpGet]
        public HttpResponseMessage GetDocuments(string GUID)
        {
            try
            {
                int _empid = _globalrepository.GetEmployeeKey(GUID).EmpId;
                List<EmployeeDocumentViewModel> _model = _employeedocumentrepository.GetDocuments(_empid);
                if (_model != null) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.ToString());
            }
        }

        [Route("api/Employee/ManageDocument")]
        [HttpPost]
        public HttpResponseMessage ManageDocument([FromBody] EmployeeDocumentModel model)
        {
            try
            {
                int result = _employeedocumentrepository.Manage(model);
                if (result > 0) { return Request.CreateResponse(HttpStatusCode.OK, "Ok."); }
                else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to save record."); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }
        //=================================DOCUMENT==========================================
    }
}
