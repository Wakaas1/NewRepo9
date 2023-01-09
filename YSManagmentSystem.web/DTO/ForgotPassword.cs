using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace YSManagmentSystem.web.DTO
{
    public class ForgotPassword
    {
        [Display(Name = "User Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Email Id Required")]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string NewPassword { get; set; }

        [Required]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }
    }
}

