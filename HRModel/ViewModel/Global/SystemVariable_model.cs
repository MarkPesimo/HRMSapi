using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class SystemVariable_model
    {
        public static class AppModuleId
        {
            public const int ISEARCH = 1;
            public const int IHOPS = 2;
            public const int HRMS = 3;
            public const int MIPAY = 4;
            public const int BCS = 5;
            public const int TAMS = 6;
        }

        public static class AppModuleName
        {
            public const string HRMS = "HRMS";
            public const string ISEARCH = "iSEARCH";
            public const string MIPAY = "MiPAY";
            public const string BCS = "BCS";
            public const string TAMS = "TAMS";
        }

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

    }
}
