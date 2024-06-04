using BO.Dtos;

namespace Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsArticleDto>> GetNews();

        Task<NewsArticleDto> GetNewsById(string id);
    }
}
