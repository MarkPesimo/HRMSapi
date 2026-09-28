using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Helpdesk
{
    public class Helpdesk_model
    {
        public class HelpdeskMonitoring_model
        {
            public int Id { get; set; }
            public DateTime ConcernDate { get; set; }
            public string ClientName { get; set; }
            public string ConcernType { get; set; }
            public string ConcernDescriprion { get; set; }
            public string FileStatus { get; set; }
            public string ConcernStatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? DateCompleted { get; set; }
            public int Age { get; set; }
            public DateTime? DatePosted { get; set; }
        }

        public class HelpdeskMonitoringFilter_model
        {
            public int UserId { get; set; }
            public string UserType { get; set; }
            public bool  ByConcernType { get; set; }
            public int ConcernTypeID { get; set; }
            public bool ByStatus { get; set; }
            public string ConcernStatus { get; set; }
            public bool ByDate { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public string Keyword { get; set; }
            public bool ByClient { get; set; }
            public int ClientId { get; set; }
            public int CompanyId { get; set; }
        }

        public class HelpdeskConcern_model
        {
            public int Id { get; set; }
            public int PortalUserId { get; set; }
            public string UserType { get; set; }
            public int ConcernTypeId { get; set; }
            public string ConcernDescription { get; set; }
            public DateTime DateCreated { get; set; }
            public int UserId { get; set; }
            public int Mode { get; set; }
        }

        public class HelpdeskConcernThread_model
        {
            public int Id { get; set; }
            public int ConcernId { get; set; }
            public string UserType { get; set; }
            public string Remarks { get; set; }
            public string FileLocation { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public DateTime DateUploaded { get; set; }
            public int ConcernTypeId { get; set; } 
            public int UserId { get; set; }
            public int Mode { get; set; }
        }

        public class ConcernThread
        {
            public decimal Sort { get; set; }
            public int Id { get; set; }
            public string Username { get; set; }
            public DateTime DateCreated { get; set; }
            public string Remarks { get; set; }
            public string Usertype { get; set; }
            public string TimeCreated { get; set; }
        }
    }
}
