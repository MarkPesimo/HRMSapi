using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Employees.Document
{
    public class RequiredDocument_model
    {
        public class Monitoring_model
        {
            public int EmpId { get; set; }
            public string ClientName { get; set; }
            public string EmpNo { get; set; }
            public string EmployeeName { get; set; }
            public string Remarks { get; set; }
            public string MissingDocument { get; set; }
            public string DateHired { get; set; }
            public string ContractStart { get; set; }
            public string ContractEnd { get; set; }
            public int Age { get; set; }
        }

        public class RequiredDocument_list_model
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string WithSimilarDocument { get; set; }
            public string IsRequired { get; set; }
            public string CreatedBy { get; set; }
            public string DateCreated { get; set; }
        }

        public class RequiredDocument
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string WithSimilarDocument { get; set; }
            public bool IsRequired { get; set; }
            public int UserId { get; set; }
            public int Mode { get; set; }
        }


        public class SimilarDocument
        {
            public int Id { get; set; }
            public int RequiredDocumentId { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public int UserId { get; set; }
            public string CreatedBy { get; set; }
            public int Mode { get; set; }
            public DateTime DateCreated { get; set; }
        }
    }
}
