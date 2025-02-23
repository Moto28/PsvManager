using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.UI.Shared.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email address required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}
