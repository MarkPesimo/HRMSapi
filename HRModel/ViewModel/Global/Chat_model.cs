using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace HRModel.ViewModel.Global
{
    public class ChatMessageModel
    {
        public long MessageId { get; set; }
        public string ChatType { get; set; } // "direct" or "group"
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string GroupName { get; set; }
        public string MessageText { get; set; }
        public bool IsSystem { get; set; }
        public DateTime SentAt { get; set; }
    }

    public class ChatGroupInfoModel
    {
        public string GroupName { get; set; }
        public string CreatedBy { get; set; }
        public List<string> Members { get; set; } = new List<string>();
    }

    public class CreateGroupRequestModel
    {
        public string GroupName { get; set; }
        public string CreatedBy { get; set; }
        public List<string> Members { get; set; } = new List<string>();
    }

    public class ManageGroupMemberRequestModel
    {
        public string GroupName { get; set; }
        public string ActorUsername { get; set; }
        public string TargetUsername { get; set; }
        public List<string> TargetUsernames { get; set; } = new List<string>();
    }
}