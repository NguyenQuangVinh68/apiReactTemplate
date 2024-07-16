using System.ComponentModel.DataAnnotations;

namespace apiReact.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Please fill id")]
        [StringLength(5,  ErrorMessage = "invalid id")]
        public string emp_no { set; get; }

        [Required(ErrorMessage = "Please fill password")]
        public string password { set; get; }
    }
}
