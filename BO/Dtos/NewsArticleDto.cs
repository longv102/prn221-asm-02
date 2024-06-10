using System.ComponentModel.DataAnnotations;

namespace BO.Dtos
{
    public class NewsArticleDto
    {
        [Required(ErrorMessage = "Article id is required.")]
        public string NewsArticleId { get; set; } = null!;

        [Required(ErrorMessage = "News title is required.")]
        [MaxLength(255, ErrorMessage = "Maximum length is 255 characters.")]
        public string? NewsTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        [Required]
        public short? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        public bool? NewsStatus { get; set; }

        [Required]
        public short? CreatedById { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        //public virtual Category? Category { get; set; }

        //public virtual SystemAccount? CreatedBy { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<string> TagNames = new List<string>();
    }
}
