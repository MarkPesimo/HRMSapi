using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRModel.ViewModel.Global
{
    public class LoginModel
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Username is as required field.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is as required field.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public int AppModuleId { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string Remarks { get; set; }
        public string CompanyLogo { get; set; }
    }
}
