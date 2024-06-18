using System.ComponentModel.DataAnnotations;

namespace BO.Dtos
{
    public class SystemAccountDto
    {
        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Account Id must be a non-negative integer.")]
        public short AccountId { get; set; }

        [Required(ErrorMessage = "Account name is required!")]  
        public string? AccountName { get; set; }

        [Required(ErrorMessage = "Account email is required!")]
        [EmailAddress]
        public string? AccountEmail { get; set; }

        public int AccountRole { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string? AccountPassword { get; set; }
    }
}
