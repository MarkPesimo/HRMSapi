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
                _model.SenderUsername,
                _model.RecipientUsername,
                _model.GroupName,
                _model.MessageText,
                _model.IsSystem
            );
        }

        public List<ChatMessageModel> GetDirectHistory(string _userA, string _userB, int _top = 100)
        {
            var rows = (from x in _conn.USP_CHAT_GET_DIRECT_HISTORY(_userA, _userB, _top)
                        select x)
                       .AsEnumerable()
                       .Select(d => new ChatMessageModel()
                       {
                           MessageId = d.MessageId,
                           ChatType = d.ChatType,
                           SenderUsername = d.SenderUsername,
                           RecipientUsername = d.RecipientUsername,
                           GroupName = d.GroupName,
                           MessageText = d.MessageText,
                           IsSystem = d.IsSystem,
                           SentAt = d.SentAt
                       }).ToList();

            rows.Reverse();
            return rows;
        }

        public List<ChatMessageModel> GetGroupHistory(string _groupName, int _top = 100)
        {
            var rows = (from x in _conn.USP_CHAT_GET_GROUP_HISTORY(_groupName, _top)
                        select x)
                       .AsEnumerable()
                       .Select(d => new ChatMessageModel()
                       {
                           MessageId = d.MessageId,
                           ChatType = d.ChatType,
                           SenderUsername = d.SenderUsername,
                           RecipientUsername = d.RecipientUsername,
                           GroupName = d.GroupName,
                           MessageText = d.MessageText,
                           IsSystem = d.IsSystem,
                           SentAt = d.SentAt
                       }).ToList();

            rows.Reverse();
            return rows;
        }

        public List<string> CreateGroup(string _groupName, string _createdBy, List<string> _members)
        {
            string cleanGroup = _groupName.Trim();
            string creator = _createdBy.Trim();

            string membersCsv = (_members != null && _members.Count > 0)
                ? string.Join(",", _members.Where(m => !string.IsNullOrWhiteSpace(m)).Select(m => m.Trim()))
                : null;

            _conn.USP_CHAT_CREATE_GROUP(cleanGroup, creator, membersCsv);

            return (from m in _conn.ChatGroupMembers
                    where m.GroupName == cleanGroup
                    orderby m.Username
                    select m.Username).ToList();
        }

        public List<string> ManageGroupMember(string _action, string _groupName, string _username)
        {
            string cleanGroup = _groupName.Trim();

            _conn.USP_CHAT_MANAGE_GROUP_MEMBER(_action, cleanGroup, _username.Trim());

            return (from m in _conn.ChatGroupMembers
                    where m.GroupName == cleanGroup
                    orderby m.Username
                    select m.Username).ToList();
        }

        public List<ChatGroupInfoModel> GetUserGroups(string _username)
        {
            var groups = (from g in _conn.ChatGroupMembers
                          where g.Username == _username
                          select g.GroupName)
                         .Distinct()
                         .OrderBy(n => n)
                         .ToList();

            return groups.Select(grp => new ChatGroupInfoModel()
            {
                GroupName = grp,
                Members = (from m in _conn.ChatGroupMembers
                           where m.GroupName == grp
                           orderby m.Username
                           select m.Username).ToList()
            }).ToList();
        }
    }
}
