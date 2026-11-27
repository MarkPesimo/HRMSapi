using HRModel.ViewModel.Global;
using HRMS_API.Helper;
using HRMS.REPO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HRMS_API.Controllers
{
    [BasicAuthentication]
    public class ChatController : ApiController
    {
        private ChatRepository _chatrepository { get; set; }

        public ChatController()
        {
            if (_chatrepository == null) { _chatrepository = new ChatRepository(); }
        }

        [Route("api/Chat/SaveMessage")]
        [HttpPost]
        public HttpResponseMessage SaveMessage([FromBody] ChatMessageModel _model)
        {
            try
            {
                if (_model == null || string.IsNullOrWhiteSpace(_model.MessageText))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid message.");
                }

                _chatrepository.SaveMessage(_model);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true });
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Chat/GetRecipientsList/{_senderuserid}")]
        [HttpGet]
        public HttpResponseMessage GetRecipientsList(int _senderuserid)
        {
            try
            {
                List<ChatMessageModel> _model = _chatrepository.GetRecipientsList(_senderuserid);
                if (_model != null && _model.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _model); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        [Route("api/Chat/GetDirectChatHistory/{userA}/{userB}/{_top}")]
        [HttpGet]
        public HttpResponseMessage GetDirectChatHistory(int userA, int userB, int _top)
        {
            try
            {
                List<ChatMessageModel> _obj = _chatrepository.GetDirectChatHistory(userA, userB, _top);

                if (_obj != null && _obj.Count > 0) { return Request.CreateResponse(HttpStatusCode.OK, _obj); }
                else { return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found!"); }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }

        //[Route("api/Chat/GetGroupHistory/{groupName}/{top:int?}")]
        //[HttpGet]
        //public HttpResponseMessage GetGroupHistory(string groupName, int top = 100)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(groupName))
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Group name is required.");
        //        }

        //        List<ChatMessageModel> _obj = _chatrepository.GetGroupHistory(groupName.Trim(), top);
        //        return Request.CreateResponse(HttpStatusCode.OK, _obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
        //    }
        //}

        //[Route("api/Chat/GetMyGroups/{username}")]
        //[HttpGet]
        //public HttpResponseMessage GetMyGroups(string username)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(username))
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username is required.");
        //        }

        //        List<ChatGroupInfoModel> _obj = _chatrepository.GetUserGroups(username.Trim());
        //        return Request.CreateResponse(HttpStatusCode.OK, _obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
        //    }
        //}



        //[Route("api/Chat/CreateGroup")]
        //[HttpPost]
        //public HttpResponseMessage CreateGroup([FromBody] CreateGroupRequestModel _model)
        //{
        //    try
        //    {
        //        if (_model == null || string.IsNullOrWhiteSpace(_model.GroupName) || string.IsNullOrWhiteSpace(_model.CreatedBy))
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Group name and creator are required.");
        //        }

        //        List<string> members = _chatrepository.CreateGroup(_model.GroupName, _model.CreatedBy, _model.Members);
        //        var result = new ChatGroupInfoModel
        //        {
        //            GroupName = _model.GroupName.Trim(),
        //            CreatedBy = _model.CreatedBy.Trim(),
        //            Members = members
        //        };

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
        //    }
        //}

        //[Route("api/Chat/ManageGroupMember")]
        //[HttpPost]
        //public HttpResponseMessage ManageGroupMember([FromBody] ManageGroupMemberRequestModel _model)
        //{
        //    try
        //    {
        //        if (_model == null || string.IsNullOrWhiteSpace(_model.GroupName) || string.IsNullOrWhiteSpace(_model.TargetUsername))
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Group name and target member are required.");
        //        }

        //        string action = string.IsNullOrWhiteSpace(_model.ActorUsername) ? "ADD" : _model.ActorUsername.Trim().ToUpperInvariant();
        //        if (action != "ADD" && action != "REMOVE")
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Action must be ADD or REMOVE.");
        //        }

        //        List<string> members = _chatrepository.ManageGroupMember(action, _model.GroupName, _model.TargetUsername);
        //        var result = new ChatGroupInfoModel
        //        {
        //            GroupName = _model.GroupName.Trim(),
        //            Members = members
        //        };

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
        //    }
        //}
    }
}