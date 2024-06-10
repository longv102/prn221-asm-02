using System.ComponentModel.DataAnnotations;

namespace BO.Dtos
{
    public class CategoryDto
    {
        public short CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; } = null!;

        [MaxLength(300, ErrorMessage = "Maximum character is 300 words.")]
        public string CategoryDesciption { get; set; } = null!;
    }
}
