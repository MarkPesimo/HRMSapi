using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class AccessModel
    {
        public static class Feature
        {
            public const string Add = "ADD";
            public const string Edit = "EDIT";
            public const string Delete = "DELETE";
            public const string Print = "PRINT";
            public const string System = "SYSTEM";
            public const string Post = "POST";
            public const string Unpost = "UNPOST";
            public const string Void = "VOID";
        }

        public static class SystemModuleType
        {
            public const int Masterfile = 1;
            public const int Transaction = 2;
            public const int Inquiry = 3;
            public const int Report = 4;
            public const int System = 5;
            public const int Process = 6;
        }

        public class ModuleAccess_model
        {
            public string ModuleName { get; set; }
            public int ModuleTypeId { get; set; }
            public int UserId { get; set; }
            public string UserType { get; set; }
            public int AppId { get; set; }
        }

        public class FunctionAccess_model
        {
            public string ModuleName { get; set; }
            public string Action { get; set; }
            public int ModuleTypeId { get; set; }
            public int UserId { get; set; }
            public string UserType { get; set; }
            public int AppId { get; set; }
            public string AppName { get; set; }
        }
    }
}
