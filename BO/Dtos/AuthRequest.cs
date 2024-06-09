using System.ComponentModel.DataAnnotations;

namespace BO.Dtos
{
    public class AuthRequest
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
