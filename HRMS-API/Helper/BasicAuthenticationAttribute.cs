using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HRMS_API.Helper
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationtoken = actionContext.Request.Headers.Authorization.Parameter;

                string[] _Array = authenticationtoken.Split(':');

                string _requestkey = _Array[0].ToString();

                List<string> _secrets = new List<string>
                {
                    //SecretKeys.ISEARCH_TOKEN,
                    //SecretKeys.HRMS_TOKEN,
                    //SecretKeys.MIPAY_TOKEN,
                    //SecretKeys.BCS_TOKEN,
                    //SecretKeys.PORTAL_TOKEN,
                    //SecretKeys.CAREERS_TOKEN,
                    //SecretKeys.CLIENT_TOKEN,
                    //SecretKeys.COOR_TOKEN,
                    //SecretKeys.ACAP_TOKEN
                };

                bool _found = false;

                foreach (string _secret in _secrets)
                {
                    if (_requestkey == _secret)
                    {
                        _found = true;
                        return;
                    }
                }

                if (!_found)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}