using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRModel.ViewModel.Global;

namespace HRMS.REPO.Repository
{
    public class ChatRepository
    {
        private apwdbEntities _conn { get; set; }

        public ChatRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
        }

        public void SaveMessage(ChatMessageModel _model)
        {
            if (_model == null) return;

            _conn.USP_CHAT_SAVE_MESSAGE(
                _model.ChatType,
                _model.MessageId,
                _model.SenderUserId,
                _model.RecipientUserId,
                _model.GroupChatId,
                _model.MessageText,
                _model.Mode
            );
        }

        public List<ChatMessageModel> GetRecipientsList(int _loginuserid)
        {
            return (from x in _conn.ChatRecipientsHistories
                    where x.SenderUserId == _loginuserid || x.RecipientUserId == _loginuserid
                    let otherUserId = (x.SenderUserId == _loginuserid) ? (x.RecipientUserId ?? 0) : x.SenderUserId
                    join u in _conn.SYS_USER on otherUserId equals u.id
                    select new
                    {
                        History = x,
                        User = u,
                        OtherUserId = otherUserId
                    })
                    .AsEnumerable()
                    .Select(d => new ChatMessageModel()
                    {
                        Id = int.Parse(d.History.id.ToString()),
                        ChatType = d.History.ChatType,
                        RecipientUserId = d.OtherUserId ?? 0,
                        RecipientName = d.User.emp_name,
                        GroupChatId = d.History.GroupChatId ?? 0
                    }).ToList();
        }

        public List<ChatMessageModel> GetDirectChatHistory(int _userA, int _userB, int _top)
        {
            var rows = (from x in _conn.USP_CHAT_GET_DIRECT_HISTORY(_userA, _userB, _top)
                        select x)
                       .AsEnumerable()
                       .Select(d => new ChatMessageModel()
                       {
                           MessageId = d.MessageId,
                           ChatType = d.ChatType,
                           SenderUserId = d.SenderUserId,
                           RecipientUserId= d.RecipientUserId ?? 0,
                           GroupChatId = d.GroupChatId ?? 0,
                           MessageText = d.MessageText,
                           SentAt = d.SentAt
                       }).ToList();

            rows.Reverse();
            return rows;
        }

        //public List<ChatMessageModel> GetGroupHistory(string _groupName, int _top = 100)
        //{
        //    var rows = (from x in _conn.USP_CHAT_GET_GROUP_HISTORY(_groupName, _top)
        //                select x)
        //               .AsEnumerable()
        //               .Select(d => new ChatMessageModel()
        //               {
        //                   MessageId = d.MessageId,
        //                   ChatType = d.ChatType,
        //                   SenderUsername = d.SenderUsername,
        //                   RecipientUsername = d.RecipientUsername,
        //                   GroupName = d.GroupName,
        //                   MessageText = d.MessageText,
        //                   IsSystem = d.IsSystem,
        //                   SentAt = d.SentAt
        //               }).ToList();

        //    rows.Reverse();
        //    return rows;
        //}

        //public List<string> CreateGroup(string _groupName, string _createdBy, List<string> _members)
        //{
        //    string cleanGroup = _groupName.Trim();
        //    string creator = _createdBy.Trim();

        //    string membersCsv = (_members != null && _members.Count > 0)
        //        ? string.Join(",", _members.Where(m => !string.IsNullOrWhiteSpace(m)).Select(m => m.Trim()))
        //        : null;

        //    _conn.USP_CHAT_CREATE_GROUP(cleanGroup, creator, membersCsv);

        //    return (from m in _conn.ChatGroupMembers
        //            where m.GroupName == cleanGroup
        //            orderby m.Username
        //            select m.Username).ToList();
        //}

        //public List<string> ManageGroupMember(string _action, string _groupName, string _username)
        //{
        //    string cleanGroup = _groupName.Trim();

        //    _conn.USP_CHAT_MANAGE_GROUP_MEMBER(_action, cleanGroup, _username.Trim());

        //    return (from m in _conn.ChatGroupMembers
        //            where m.GroupName == cleanGroup
        //            orderby m.Username
        //            select m.Username).ToList();
        //}

        //public List<ChatGroupInfoModel> GetUserGroups(string _username)
        //{
        //    var groups = (from g in _conn.ChatGroupMembers
        //                  where g.Username == _username
        //                  select g.GroupName)
        //                 .Distinct()
        //                 .OrderBy(n => n)
        //                 .ToList();

        //    return groups.Select(grp => new ChatGroupInfoModel()
        //    {
        //        GroupName = grp,
        //        Members = (from m in _conn.ChatGroupMembers
        //                   where m.GroupName == grp
        //                   orderby m.Username
        //                   select m.Username).ToList()
        //    }).ToList();
        //}
    }
}
