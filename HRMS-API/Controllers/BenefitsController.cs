using HRMS_API.Helper;
using HRMS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static HRModel.ViewModel.Employees.Benefits.Benefits_model;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]

    public class BenefitsController : ApiController
    {
        private BenefitsRepository _benefitsrepository { get; set; }

        public BenefitsController()
        {
            if (_benefitsrepository == null) { _benefitsrepository = new BenefitsRepository(); }
        }

        [Route("api/Benefits/GetBenefitsMonitoring/{Keyword}/{ByClient}/{ClientID}/{PageNo}/{PageSize}/{CompanyID}")]
        [HttpGet]
        public HttpResponseMessage GetBenefitsMonitoring(string Keyword, bool ByClient, int ClientID, bool ByDate, DateTime DateFrom, DateTime DateTo, int PageNo, int PageSize, int CompanyID)
        {
            try
            {
                List<BenefitsMonitoring_model> _model = _benefitsrepository.GetBenefitsMonitoring(Keyword, ByClient, ClientID, ByDate, DateFrom, DateTo, PageNo, PageSize, CompanyID);
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

        [Route("api/Benefits/BenefitsType")]
        [HttpGet]
        public HttpResponseMessage BenefitsType()
        {
            try
            {
                List<BenefitsType_model> results = _benefitsrepository.GetBenefitsType();

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Benefits/BenefitsClass")]
        [HttpGet]
        public HttpResponseMessage BenefitsClass()
        {
            try
            {
                List<BenefitsClass_model> results = _benefitsrepository.GetBenefitsClass();

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/Benefits/BenefitsCategory")]
        [HttpGet]
        public HttpResponseMessage BenefitsCategory()
        {
            try
            {
                List<BenefitsCategory_model> results = _benefitsrepository.GetBenefitsCategory();

                return results != null && results.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, results)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}